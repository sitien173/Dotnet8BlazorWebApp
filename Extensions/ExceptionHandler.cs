using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using Humanizer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace BlazorWebApp.Extensions;

public static class ExceptionHandler
{
    private static readonly JsonSerializerSettings _serializerSettings = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore
    };
    public static void UseFluentValidationExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature?.Error;
                var localizer = context.RequestServices.GetRequiredService<IStringLocalizer<SharedResource>>();
                switch (exception)
                {
                    case ValidationException validationException:
                    {
                        ProblemDetails response = new()
                        {
                            Title = localizer[SharedResource.ValidationFailedException],
                            Status = 400,
                            Detail = validationException.Errors.Humanize(),
                            Instance = exceptionHandlerPathFeature?.Path
                        };
                        response.Extensions.Add("errors", validationException.Errors);
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(response, _serializerSettings));
                        break;
                    }
                    case ArgumentException argumentException:
                    {
                        string cleanMessage = argumentException.Message[..argumentException.Message.IndexOf(" (Parameter", StringComparison.Ordinal)];
                        cleanMessage = cleanMessage.Humanize();
                        ProblemDetails response = new()
                        {
                            Title = localizer[SharedResource.ValidationFailedException],
                            Status = 400,
                            Detail = cleanMessage,
                            Instance = exceptionHandlerPathFeature?.Path
                        };
                        response.Extensions.Add("errors", new List<ValidationFailure>()
                        {
                            new (argumentException.ParamName, cleanMessage)
                        });
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(response, _serializerSettings));
                        break;
                    }
                    default:
                    {
                        string message = exception!.InnerException?.Message ?? exception.Message;
                        message = message.Humanize();
                        ProblemDetails response = new()
                        {
                            Title = localizer[SharedResource.DefaultTitleException],
                            Status = 500,
                            Detail = message,
                            Instance = exceptionHandlerPathFeature?.Path
                        };
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(response, _serializerSettings));
                        break;
                    }
                }
            });
        });
    }
}