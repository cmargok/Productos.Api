using Microsoft.EntityFrameworkCore;

namespace Productos.API.Utils
{
    public static class PagingExtention
    {/// <summary>
     /// Método para páginar la consulta 
     /// </summary>
     /// <typeparam name="T"></typeparam>
     /// <param name="query"></param>
     /// <param name="page">Página que se desea consultar</param>
     /// <param name="take">Cantidad de elementos a tomar</param>
     /// <returns>Colleccion de elementos presentes en la página actual</returns>
        public static async Task<DataCollection<T>> GetPagedAsync<T>(
            this IQueryable<T> query, int page, int take = 20)
        {
            var originalpage = page;
            page--;
            if (page > 0)
            {
                page *= take;
            }
            var result = new DataCollection<T>()
            {
                Items = await query.Skip(page).Take(take).ToListAsync(),
                Total = await query.CountAsync(),
                Page = originalpage,
            };

            result.Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.Total) / take));
            return result;
        }
        /// <summary>
        /// Método para obtener el conjunto de elementos resulaltes tras saltar {skip} elementos
        /// y tomar {take} elementos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="skip">Cantidad de elementos a saltar</param>
        /// <param name="take">Cantidad de elementos a tomar</param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> ToListBySkipAsync<T>(
            this IQueryable<T> query, int skip, int take)
        {
            int _take = take > 0 ? take : 20;
            int _skip = skip > 0 ? skip : 0;
            return await query.Skip(_skip).Take(_take).ToListAsync();
        }

        public class DataCollection<T>
        {
            public bool HasItems
            {
                get
                {
                    if (Items == null) return false;

                    return Items.Any();
                }
            }
            public IEnumerable<T> Items { get; set; }
            public int Total { get; set; }
            public int Page { get; set; }
            public int Pages { get; set; }
        }

    }
}
