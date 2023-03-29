namespace Productos.API.Handlers.ErrorHandling.CustomExceptions
{
    public class ValidationDataException : Exception 
    {
        /// <summary>
        /// Diccionario con los errores encontrados al momento de validar un objeto
        public IDictionary<string, string[]>? Errores { get; set; }

        public ValidationDataException(string message): base (message)
        {
        }
        /// <summary>
        /// Representa errores al momento de validar segun las reglas de negocio
        /// </summary>
        /// <param name="_errores"></param>
        /// <param name="message"></param>
        public ValidationDataException(IDictionary<string, string[]> _errores, string message) : base(message)       
        {

            Errores = _errores;
        }

    }
}
