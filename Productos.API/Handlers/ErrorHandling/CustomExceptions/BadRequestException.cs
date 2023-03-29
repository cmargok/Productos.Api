namespace Productos.API.Handlers.ErrorHandling.CustomExceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException()
        {

        }

        /// <summary>
        /// Representa errores cuando se hace una peticio erronea al sistema
        /// </summary>
        /// <param name="menssage"></param>
        public BadRequestException(string menssage) : base(menssage)
        {

        }
    }
}
