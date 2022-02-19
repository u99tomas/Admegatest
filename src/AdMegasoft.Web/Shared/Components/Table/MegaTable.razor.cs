using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AdMegasoft.Web.Shared.Components.Table
{
    public partial class MegaTable<T>
    {
        [Parameter]
        public RenderFragment ToolBarContent { get; set; }

        [Parameter]
        public RenderFragment HeaderContent { get; set; }

        [Parameter]
        public RenderFragment<T> RowTemplate { get; set; }

        [Parameter]
        public Func<TableState, Task<TableData<T>>> ServerData { get; set; }

        private bool _loading { get; set; } = false;

        private MudTable<T> _table;

        private async Task<TableData<T>> ServerReload(TableState state)
        {
            ToggleLoading();
            var tableData = await ServerData(state);
            ToggleLoading();

            return tableData;
        }

        private void ToggleLoading()
        {
            _loading = !_loading;
            StateHasChanged();
        }

        public void ReloadServerData()
        {
            _table.ReloadServerData();
        }

    }
}
