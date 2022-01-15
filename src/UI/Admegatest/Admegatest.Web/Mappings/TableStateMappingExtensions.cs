using Admegatest.Services.Helpers.Pagination;
using MudBlazor;

namespace Admegatest.Web.Mappings
{
    public static class TableStateMappingExtensions
    {
        public static AdmTableState ToAdmTableState(this TableState tableState, string searchString)
        {
            return new AdmTableState
            {
                SortDirection = (Data.Enums.SortDirection)tableState.SortDirection,
                PageNumber = tableState.Page,
                PageSize = tableState.PageSize,
                SearchString = searchString,
                SortLabel = tableState.SortLabel,
            };
        }
    }
}
