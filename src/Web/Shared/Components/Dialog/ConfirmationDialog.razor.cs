using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Web.Shared.Components.Dialog
{
    public partial class ConfirmationDialog
    {
        [CascadingParameter]
        private MudDialogInstance _dialog { get; set; }

        private void Confirm()
        {
            _dialog.Close(DialogResult.Ok(true));
        }

        private void Cancel()
        {
            _dialog.Cancel();
        }
    }
}
