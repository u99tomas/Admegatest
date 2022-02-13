using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AdMegasoft.Web.Shared.Components.Dialogs
{
    public partial class MegaDialog
    {
        [CascadingParameter]
        private MudDialogInstance _mudDialog { get; set; }

        [Parameter]
        public RenderFragment Content { get; set; }

        [Parameter]
        public RenderFragment Title { get; set; }

        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public EventCallback OnSave { get; set; }

        private async Task SaveAsync()
        {
            await OnSave.InvokeAsync();
        }

        private void Cancel()
        {
            _mudDialog.Cancel();
        }
    }
}
