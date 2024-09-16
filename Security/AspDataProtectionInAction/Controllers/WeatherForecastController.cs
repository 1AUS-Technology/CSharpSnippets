using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace AspDataProtectionInAction.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(IDataProtectionProvider protectionProvider, ILogger<WeatherForecastController> logger) : ControllerBase
{
    // each protector for a purpose
    private readonly IDataProtector _realmProtector = protectionProvider.CreateProtector(ProtectionPurpose);
    private const string ProtectionPurpose = "QRSecret";

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        string mySecret = "42";
        string protectedPayload = _realmProtector.Protect(mySecret);

        logger.LogInformation("Protected payload: " + protectedPayload );

        var unprotected = _realmProtector.Unprotect(protectedPayload);

        logger.LogInformation("Unprotected payload: " + unprotected);

        return new WeatherForecast[]{};

    }
}