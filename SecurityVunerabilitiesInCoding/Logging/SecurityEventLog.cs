namespace AdvancedAspnetCoreSecurity.Logging;

public class SecurityEventLog
{
    public int SecurityLevel { get; set; }
    public int EventId { get; set; }
    public string? LoggedInUserId { get; set; }
    public string? RequestIpaddress { get; set; }
    public int RequestPort { get; set; }
    public PathString RequestPath { get; set; }
    public string RequestQuery { get; set; }
    public string UserAgent { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public string AdditionalInfo { get; set; }
}