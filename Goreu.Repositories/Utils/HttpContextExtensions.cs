using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Repositories.Utils
{
    public static class HttpContextExtensions
    {
        public async static Task InsertarPaginacionHeader<T>(this HttpContext httpContext, IQueryable<T> queryable)
        {
            if (httpContext is null)
                throw new ArgumentNullException(nameof(httpContext));

            double totalRecords = await queryable.CountAsync();
            httpContext.Response.Headers.Add("TotalRecordsQuantity", totalRecords.ToString());
        }
    }
}
