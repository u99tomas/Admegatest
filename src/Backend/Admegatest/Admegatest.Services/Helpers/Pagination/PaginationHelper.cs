using Microsoft.EntityFrameworkCore;

namespace Admegatest.Services.Helpers.Pagination
{
    public class PaginationHelper
    {
        //public IQueryable<T> Queryable { get; set; }
        //private readonly AdmTableState _admTableState;

        //public PaginationHelper(IQueryable<T> queryable, AdmTableState admTableState)
        //{
        //    Queryable = queryable;
        //    _admTableState = admTableState;
        //}

        //public async Task<AdmTableData<T>> GetTableDataAsync(string[] searchFields, string[] sortLabels)
        //{

        //}

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
