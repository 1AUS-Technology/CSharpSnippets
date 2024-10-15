using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;
using SecurityVunerabilitiesInCoding.Data;

namespace AdvancedAspnetCoreSecurity.Logging;

public class SecurityEventType(int eventId, SecurityEventType.SecurityLevel eventLevel)
{
    public enum SecurityLevel
    {
        Unknown = 1,
        Success = 2,
        Audit = 3,
        Info = 4,
        Warning = 5,
        Error = 6,
        Critical = 7
    }

    public int EventId { get; } = eventId;
    public SecurityLevel EventLevel { get; } = eventLevel;
}

public class SecurityLogger : ISecurityLogger
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _debugLogger;
    private readonly HttpContext? _httpContext;

    private readonly UserManager<IdentityUser> _userManager;

    //Constructor if you don’t have debug logger
    public SecurityLogger(ApplicationDbContext dbContext,
        IHttpContextAccessor httpAccessor,
        UserManager<IdentityUser> userManager)
        : this(dbContext, null, httpAccessor, userManager)
    {
    }

    public SecurityLogger(ApplicationDbContext dbContext, ILogger debugLogger, IHttpContextAccessor httpAccessor, UserManager<IdentityUser> userManager)
    {
        _dbContext = dbContext;
        _debugLogger = debugLogger;
        _httpContext = httpAccessor.HttpContext;
        _userManager = userManager;
    }

    public void LogEvent(LogLevel debugLevel, SecurityEventType securityEvent, string message)
    {
        IHeaderDictionary headers = _httpContext?.Request.Headers ?? new HeaderDictionary(new Dictionary<string, StringValues>());
        string userAgent = headers.ContainsKey("User-Agent") ? "" : headers["User-Agent"].ToString()[..1000];

        var newEvent = new SecurityEventLog
        {
            SecurityLevel = (int)securityEvent.EventLevel,
            EventId = securityEvent.EventId,
            LoggedInUserId = _httpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
            RequestIpaddress = _httpContext.Connection.RemoteIpAddress?.ToString(),
            RequestPort = _httpContext.Connection.RemotePort,
            RequestPath = _httpContext.Request.Path,
            RequestQuery = _httpContext.Request.QueryString.ToString(),
            CreatedDateTime = DateTime.UtcNow,
            AdditionalInfo = message,
            UserAgent = userAgent
        };


        _dbContext.SecurityEventLogs.Add(newEvent);
        _dbContext.SaveChanges();
        //Code that calls the debug framework
        //if the _debugLogger is not null
        //should go here
    }
}

public interface ISecurityLogger
{
    void LogEvent(LogLevel debugLevel, SecurityEventType securityEvent, string message);
}

public class SecurityEvent
{
    public class Authentication
    {
        public static SecurityEventType LoginSuccessful { get; } = new(1200, SecurityEventType.SecurityLevel.Success);

        public static SecurityEventType LogoutSuccessful { get; } = new(1201,
            SecurityEventType.SecurityLevel.Success);

        public static SecurityEventType PasswordMismatch { get; } = new(1202,
            SecurityEventType.SecurityLevel.Info);

        public static SecurityEventType UserLockedOut { get; } = new(1203,
            SecurityEventType.SecurityLevel.Warning);

        public static SecurityEventType UserNotFound { get; } = new(1204, SecurityEventType.SecurityLevel.Warning);

        public static SecurityEventType LoginSuccess2FaRequired { get; } = new(1210, SecurityEventType.SecurityLevel.Info);
//More authentication event types here
    }
//Other classes with other event types here
}