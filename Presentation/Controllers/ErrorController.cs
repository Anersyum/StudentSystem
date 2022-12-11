﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace StudentTestPontis.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]")]
[ApiController]
public sealed class ErrorController : ControllerBase
{
    // todo: create better exception handling
    public IActionResult Get()
    {
        var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionFeature != null)
        {
            var exceptionMessage = exceptionFeature.Error.InnerException?.Message ?? exceptionFeature.Error.Message;

            return BadRequest(exceptionMessage);
        }

        ProblemDetails problem = new()
        {
            Title = "Error",
            Detail = "Something went wrong! Contact site administrator!",
            Status = 500
        };

        return StatusCode(500, problem);
    }
}