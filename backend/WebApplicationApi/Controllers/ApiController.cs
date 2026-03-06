using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        // Cambio conceptual: devolver ActionResult<T> y dejar que el caller gestione el objeto T o errores
        public static ObjectResult ControllerBaseValidation<T>(Response<T> response, ModelStateDictionary? modelState = null, int? statusCode = null)
        {
            if (modelState?.IsValid == false)
            {
                var errors = modelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? e.Exception?.Message ?? "Invalid value" : e.ErrorMessage)
                    .ToList();

                return new BadRequestObjectResult(new { Message = "Validation Failed", Errors = errors });
            }

            if (statusCode.HasValue && statusCode.Value is >= 100 and <= 599)
            {
                return new ObjectResult(response) { StatusCode = statusCode.Value };
            }

            // Wrap the response in an ObjectResult to match the return type
            return new ObjectResult(response);
        }
    }
}
