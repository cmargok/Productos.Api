using System.Net;

namespace Productos.API.Handlers.ErrorHandling.CustomExceptions
{
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Diccionario con los errores encontrados al momento de validar o en el flujo de informacion para encontrar el registro
        /// </summary>
        public Dictionary<string, object>? Errores { get; set; }

        public NotFoundException()
        {

        }
        public NotFoundException(string menssage) : base(menssage)
        {

        }

        /// <summary>
        /// Representa errores cuando se hace un peticion erronea al sistema, diseñada para error HTTP 404
        /// </summary>
        /// <param name="_errores"></param>
        /// <param name="menssage"></param>
        public NotFoundException(Dictionary<string, object> _errores, string menssage) : base(menssage)
        {
            Errores = _errores;
        }
    }
}
