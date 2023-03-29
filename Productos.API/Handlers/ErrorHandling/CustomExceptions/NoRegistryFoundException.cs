namespace Productos.API.Handlers.ErrorHandling.CustomExceptions
{
    public class NoRegistryFoundException: Exception
    {
        /// <summary>
        /// Diccionario con los errores encontrados al momento de validar o en el flujo de informacion para encontrar el registro
        /// </summary>
        public Dictionary<string, object>? Errores { get; set; }

        public NoRegistryFoundException()
        {

        }
        /// <summary>
        /// Representa errores cuando no se encuentra un registro
        /// </summary>
        /// <param name="menssage"></param>
        public NoRegistryFoundException(string menssage): base(menssage)
        {

        }

        /// <summary>
        /// Representa erroes cuando no se encuentra un registro o falla las validaciones al recibir un diccionario de errores
        /// </summary>
        /// <param name="_errores"></param>
        /// <param name="menssage"></param>
        public NoRegistryFoundException(Dictionary<string, object> _errores, string menssage) : base(menssage)
        {
            Errores = _errores;
        }
    }
}
