namespace Productos.API.Handlers.ErrorHandling.CustomExceptions
{
    public class UnfinishiedOperationException : Exception
    {
        /// <summary>
        /// Diccionario con los errores encontrados al momento de validar o en el flujo de informacion para encontrar el registro o
        /// </summary>
        public Dictionary<string, object>? Errores { get; set; }
        public UnfinishiedOperationException()
        {

        }
        /// <summary>
        /// Representa errores cuando no se peude seguir con el hilo de ejecucion de la operacion a efectuar
        /// </summary>
        /// <param name="menssage"></param>
        public UnfinishiedOperationException(string menssage) : base(menssage)
        {

        }
     
        public UnfinishiedOperationException(Dictionary<string, object> _errores, string menssage) : base(menssage)
        {
            Errores = _errores;
        }

    }
}
