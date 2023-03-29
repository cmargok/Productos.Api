using Microsoft.Data.SqlClient;
using System.Net;
using Productos.API.Handlers.ErrorHandling.CustomExceptions;

namespace Productos.API.Handlers.ErrorHandling.Middleware
{
    /// <summary>
    /// MiddleWare de 2do nivel para el manejo de excepciones
    /// actua como segunda y a veces como primera barrera de recepcion de excepciones para manejar un unico escudo contra la escepciones
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BadRequestException badRequestExc)
            {
                var Response = ResponsesHttpErrors.Set400(badRequestExc);
                await HandleExceptionAsync(context, (int)HttpStatusCode.BadRequest, Response);
            }
            catch (UnfinishiedOperationException UnfinishExc)
            {
                var Response = ResponsesHttpErrors.Set400(UnfinishExc);
                await HandleExceptionAsync(context, (int)HttpStatusCode.OK, Response);
            }

            catch (NotFoundException ArgunmentNullExc)
            {
                var Response = ResponsesHttpErrors.Set404(ArgunmentNullExc);
                await HandleExceptionAsync(context, (int)HttpStatusCode.OK, Response);
            }


            catch (ValidationDataException validationdataExc)
            {
                var Response = ResponsesHttpErrors.Set422(validationdataExc);
                await HandleExceptionAsync(context, (int)HttpStatusCode.BadRequest, Response);
            }

            catch (SqlException sqlException)
            {
                var Response = ResponsesHttpErrors.Set500A(sqlException, "DataBase");
                await HandleExceptionAsync(context, (int)HttpStatusCode.InternalServerError, Response);
            }          
            catch (Exception ex)
            {
                var Response = ResponsesHttpErrors.Set500(ex);
                await HandleExceptionAsync(context, (int)HttpStatusCode.InternalServerError, Response);
            }


        }



        /// <summary>
        /// Encapsula la excepcion en un response de tipo problem+json
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, int statusCode,string error)
        {

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(error);
        }

     



    }
}
