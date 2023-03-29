namespace Productos.API.Model.domains.Search
{
    public class SearchDto
    {
        private int _page = 1;
        private int _take = 20;
        public string? Producto { get; set; }  
        public string? Descripcion { get; set; }
        public string? Categoria { get; set; }
        public bool AscendingOrder { get; set; } = true;
        public string? Columna { get; set; }
     
        public int Page { get { return _page; } set { if (value > 0) _page = value; } }  
        public int Take { get { return _take; } set { if (value > 0) _take = value; } }
    }
}
