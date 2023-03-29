using Productos.API.Handlers.ErrorHandling.CustomExceptions;
using System;
using System.Net;
using System.Net.Sockets;
namespace Productos.API.Handlers.ErrorHandling.Middleware
{
    /// <summary>
    /// MiddleWare de prefiltrado o 1er nivel para el manejo de excepciones
    /// actua como primera barrera de recepcion de excepciones, atrapa excepciones mas especificas para volverlas mas genericas
    /// con el fin de simplificar los mensajes de error
    /// </summary>
    public class FilterErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public FilterErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InvalidOperationException exception) when (exception.Source!.Equals("Microsoft.EntityFrameworkCore"))
            {
                throw new UnfinishiedOperationException(exception.Message);
            }

            catch (NoRegistryFoundException notFoundException)
            {
                 throw new NotFoundException(notFoundException.Errores! , notFoundException.Message);
            }

            catch (ArgumentNullException ArgunmentNullExc) 
            {
                throw new BadRequestException(ArgunmentNullExc.Message);
            }
            catch (ArgumentException ArgumentEx) when (ArgumentEx.Source!.Equals("ClosedXML"))
            {
                throw new Exception(ArgumentEx.Message);
            }   
        }
    }
}
