﻿using AdMegasoft.Web.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AdMegasoft.Web.Shared.Components.Table
{
    public partial class MegaTable<T>
    {
        [Parameter]
        public RenderFragment ToolBar { get; set; }

        [Parameter]
        public RenderFragment Header { get; set; }

        [Parameter]
        public RenderFragment<T>? Actions { get; set; }

        [Parameter]
        public RenderFragment<T> RowTemplate { get; set; }

        [Parameter]
        public Func<MegaTableState, Task<TableData<T>>> ServerData { get; set; }

        private bool _loading { get; set; } = false;

        private string _searchString { get; set; } = String.Empty;

        private MudTable<T> _table;

        private async Task<TableData<T>> ServerReload(TableState state)
        {
            ToggleLoading();
            var megaState = new MegaTableState(state.Page, state.PageSize, state.SortDirection.ToString(), state.SortLabel, _searchString);
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