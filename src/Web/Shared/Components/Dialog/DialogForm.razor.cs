using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Web.Shared.Components.Dialog
{
    public partial class DialogForm
    {
        [CascadingParameter]
        private MudDialogInstance _dialog { get; set; }

        [Parameter]
        public RenderFragment Content { get; set; }

        [Parameter]
        public RenderFragment Title { get; set; }

        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public object Model { get; set; }

        [Parameter]
        public EventCallback OnValidSubmit { get; set; }

        private async Task Submit()
        {
            await OnValidSubmit.InvokeAsync();
        }

        private void Cancel()
        {
            _dialog.Cancel();
        }
    }
}
