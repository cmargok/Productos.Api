using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Net;
using Productos.API.Model.domains;
using Productos.API.Handlers.ErrorHandling.CustomExceptions;

namespace Productos.API.Handlers.ErrorHandling.Middleware
{
    /// <summary>
    /// Contienes los metodos con los codigos de respuesta predeterminados para el sistema, los cuales se usaran en la respuesta de errores
    /// </summary>
    internal sealed class ResponsesHttpErrors
    {
        /// <summary>
        /// Recibe una excepcion y la desgloza para enviar un codigo HTTP 400
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string Set400(Exception exception)
        {
            ResponseDTO<Dictionary<string, object>> result = new()
            {                 
                Status = (int)HttpStatusCode.BadRequest,
                Title = "Bad Request",
                Message = exception.Message,
            };
            return GetString(result);
        }

        /// <summary>
        /// Recibe una excepcion del tipo <see cref="NotFoundException"/>, la desgloza para enviar un response HTTP 404
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string Set404(NotFoundException exception)
        {
            ResponseDTO<Dictionary<string, object>> result = new()
            {
                Status = (int)HttpStatusCode.NotFound,
                Title = "Content not found",
                Message = exception.Message,
                Data = exception.Errores!,
            };
            return GetString(result); 
        }
        /// <summary>
        /// Recibe una excepcion del tipo <see cref="ValidationDataException"/>, la desgloza para enviar un response HTTP 422
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string Set422(ValidationDataException exception)
        {
            ResponseDTO<IDictionary<string, string[]>> result = new()
            {                 
                Status = (int)HttpStatusCode.UnprocessableEntity,
                Title = "One or more validations errors have been found",
                Message = exception.Message,
                Data = exception.Errores!,
            };
            return GetString(result);
        }

        /// <summary>
        /// Recibe una excepcion del tipo <see cref="Exception"/>, la desgloza para enviar un response HTTP 500, que representa un error no manejado en el sistema
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Set500(Exception exception, string source = "")
        {
            ResponseDTO<string> result = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = source + " An error had ocurred while processing your request",
                Message = exception.Message,
                Data =  exception.InnerException is null ? "" : exception.InnerException.Message 
            };
            return GetString(result);
        }


        /// <summary>
        /// Recibe una excepcion del tipo <see cref="SqlException"/>, la desgloza para enviar un response HTTP 500,representa error con SQL
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Set500A(SqlException exception, string source = "")
        {
            ResponseDTO<string> result = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = source + " An error had ocurred while processing your request",
                Message = exception.Message,
                //Data = exception.InnerException!.Message
            };
            return GetString(result);
        }












        private static string GetString(object result)
        {
            return JsonConvert.SerializeObject(result);
        }

    }
}
