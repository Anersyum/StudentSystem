using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]")]
[ApiController]
public sealed class ErrorController : ControllerBase
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }

    public IActionResult Get()
    {
        var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
        var exceptionMessage = exceptionFeature.Error.InnerException?.Message ?? exceptionFeature.Error.Message;
        
        _logger.LogError(exceptionFeature.Error, exceptionMessage);
        
        ProblemDetails problem = new()
        {
            Title = "Error",
            Detail = "Something went wrong! Contact site administrator!",
            Status = 500
        };

        return StatusCode(500, problem);
    }
}
