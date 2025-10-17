namespace Goreu.Repositories.Utils
{
    public static class HttpContextExtensions
    {

        public async static Task InsertarPaginacionHeader<T>(this HttpContext httpContext, IQueryable<T> queryable)
        {
            if (httpContext is null)
                throw new ArgumentNullException(nameof(httpContext));

            // Evita que EF haga tracking innecesario
            var totalRecords = await queryable.CountAsync();

            // 🔹 Usa un nombre de cabecera estándar
            //    (esto facilita el consumo desde Angular u otro front)
            const string headerName = "totalrecordsquantity";

            if (httpContext.Response.Headers.ContainsKey(headerName))
                httpContext.Response.Headers.Remove(headerName);

            httpContext.Response.Headers.Add(headerName, totalRecords.ToString());
        }
        
        //public async static Task InsertarPaginacionHeader<T>(this HttpContext httpContext, IQueryable<T> queryable)
        //{
        //    if (httpContext is null)
        //        throw new ArgumentNullException(nameof(httpContext));

        //    int totalRecords = await queryable.CountAsync();

        //    // Usa una convención estándar en APIs REST
        //    httpContext.Response.Headers["X-Total-Count"] = totalRecords.ToString();
        //}
    }
}
