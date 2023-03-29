namespace Productos.API.Model.domains
{
    public class ResponseDTO<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public T Data { get; set; }
    }
}
