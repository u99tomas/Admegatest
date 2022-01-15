using Microsoft.EntityFrameworkCore;

namespace Admegatest.Services.Helpers.Pagination
{
    public class PaginationHelper
    {
        public static async Task<AdmTableData<T>> GetTableDataAsync<T>(IQueryable<T> queryable, AdmTableState admTableState)
        {
            var admTableData = new AdmTableData<T>();

            admTableData.TotalItems = await queryable.CountAsync();

            admTableData.Items = await queryable.Skip(admTableState.PageNumber * admTableState.PageSize)
                .Take(admTableState.PageSize).ToListAsync();

            return admTableData;
        }
    }
}
