using AdMegasoft.Web.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AdMegasoft.Web.Shared.Components.Table
{
    public partial class PagedTable<T>
    {
        [Parameter]
        public RenderFragment ToolBar { get; set; }

        [Parameter]
        public RenderFragment Header { get; set; }

        [Parameter]
        public RenderFragment<T>? MenuTemplate { get; set; }

        [Parameter]
        public RenderFragment<T> RowTemplate { get; set; }

        [Parameter]
        public Func<PagedTableState, Task<TableData<T>>> ServerData { get; set; }

        [Parameter]
        public bool MultiSelection { get; set; } = false;

        [Parameter]
        public EventCallback<HashSet<T>> SelectedItemsChanged { get; set; }

        [Parameter]
        public HashSet<T> SelectedItems
        {
            get => _selectedItems;
            set
            {
                if (_selectedItems == value) return;
                _selectedItems = value;
                SelectedItemsChanged.InvokeAsync(value);
            }
        }

        private HashSet<T> _selectedItems;

        private bool _loading { get; set; } = false;

        private string _searchString { get; set; } = String.Empty;

        private MudTable<T> _table;

        private async Task<TableData<T>> ServerReload(TableState state)
        {
            ToggleLoading();
            var megaState = new PagedTableState(state.Page, state.PageSize, state.SortDirection.ToString(), state.SortLabel, _searchString);
            var tableData = await ServerData(megaState);
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

        private void OnSearch(string text)
        {
            _searchString = text;
            _table.ReloadServerData();
        }
    }
}
