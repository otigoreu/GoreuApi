using Goreu.Dto.Request;

namespace Goreu.Repositories.Utils
{
    public static class IQueryableExtensions
    {
        //public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDto pagination)
        //{
        //    // Aseguramos que los valores sean válidos
        //    var page = pagination.Page < 1 ? 1 : pagination.Page;
        //    var recordsPerPage = pagination.RecordsPerPage <= 0 ? 10 : pagination.RecordsPerPage;

        //    return queryable
        //        .Skip((page - 1) * recordsPerPage)
        //        .Take(recordsPerPage);
        //}

        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDto paginationDto)
        {
            return queryable
                .Skip((paginationDto.Page) * paginationDto.RecordsPerPage)
                .Take(paginationDto.RecordsPerPage);
        }
    }
}
