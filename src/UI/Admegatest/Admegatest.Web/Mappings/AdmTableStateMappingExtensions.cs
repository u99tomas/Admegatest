using Admegatest.Services.Helpers.Pagination;
using MudBlazor;

namespace Admegatest.Web.Mappings
{
    public static class AdmTableStateMappingExtensions
    {
        public static TableData<T> ToTableData<T>(this AdmTableData<T> admTableData)
        {
            return new TableData<T> { TotalItems = admTableData.TotalItems, Items = admTableData.Items, };
        }
    }
}
