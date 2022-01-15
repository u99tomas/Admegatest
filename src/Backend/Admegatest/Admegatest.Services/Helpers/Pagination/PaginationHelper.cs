using Microsoft.EntityFrameworkCore;

namespace Admegatest.Services.Helpers.Pagination
{
    public class PaginationHelper<T>
    {
        private readonly IQueryable<T> _queryable;
        private readonly AdmTableState _admTableState;

        public PaginationHelper(IQueryable<T> queryable, AdmTableState admTableState)
        {
            _queryable = queryable;
            _admTableState = admTableState;
        }

        public async Task<AdmTableData<T>> GetTableDataAsync()
        {
            var admTableData = new AdmTableData<T>();

            admTableData.TotalItems = await _queryable.CountAsync();

            admTableData.Items = await _queryable.Skip(_admTableState.PageNumber * _admTableState.PageSize)
                .Take(_admTableState.PageSize).ToListAsync();

            return admTableData;
        }
    }
}
