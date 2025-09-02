namespace Goreu.Repositories.Utils
{
    public static class HttpContextExtensions
    {
        public async static Task InsertarPaginacionHeader<T>(this HttpContext httpContext, IQueryable<T> queryable)
        {
            if (httpContext is null)
                throw new ArgumentNullException(nameof(httpContext));

            int totalRecords = await queryable.CountAsync();
            httpContext.Response.Headers["totalrecordsquantity"] = totalRecords.ToString();

            //httpContext.Response.Headers.Add("totalrecordsquantity", totalRecords.ToString());
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
