using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Productos.API.Model.domains;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Productos.API.Utils
{
    /// <summary>
    /// Sobreescritura del valor por defecto al momento de vlaidar la estructura del objeto, para adecuarse al tipo de respuesta que se implemento para el sistema
    /// </summary>
    public class CustomValidationFilterAttributecs : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            ResponseDTO<Dictionary<string, object>> response = new()
            {
                Status = 422,
                Message = "One or more problems occurred while processing your request",
                Title = "submitted information is in an incorrect format",
                Data = context.ModelState.GetErrorsToDictionary()
            };

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(response);
            }

            base.OnActionExecuting(context);
        }
    }
    public static class Extensions
    {
        /// <summary>
        /// Take the keys (parameters) and their errors from a ModelStateDictionary Into A "Dictionary"(<string, object>)  
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentExtensionsNullException"></exception>
        public static Dictionary<string, object> GetErrorsToDictionary(this ModelStateDictionary modelState)
        {
            try
            {
                Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
                if (modelState == null)
                {
                    return null!;
                }
                foreach (var keyModelStatePair in modelState)
                {
                    var key = keyModelStatePair.Key;
                    var errors = keyModelStatePair.Value.Errors;
                    if (errors != null && errors.Count > 0)
                    {
                        var errorMessages = errors.Select(error =>
                        {
                            return string.IsNullOrEmpty(error.ErrorMessage) ? "error" : error.ErrorMessage;
                        }).ToArray();

                        keyValuePairs.Add(key, errorMessages);
                    }
                }

                return keyValuePairs;
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("Errors are null or Empty or " + ex.Message);
            }

        }

    }
 

}
