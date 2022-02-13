using AdMegasoft.Web.Enums;
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
        public string EntityName { get; set; }

        [Parameter]
        public Operation Operation { get; set; }

        [Parameter]
        public EventCallback OnConfirmation { get; set; }

        [Parameter]
        public string ContentText { get; set; }

        private string Title
        {
            get
            {
                if (Operation == Operation.Add)
                {
                    return $"Crear {EntityName}";
                }
                else if (Operation == Operation.Edit)
                {
                    return $"Editar {EntityName}";
                }
                else if (Operation == Operation.Delete)
                {
                    return "Confirmación de eliminación";
                }

                return "No se ha declarado un tipo operación";
            }
        }

        private string Icon
        {
            get
            {
                if (Operation == Operation.Add)
                {
                    return Icons.Material.Filled.Add;
                }
                else if (Operation == Operation.Edit)
                {
                    return Icons.Material.Filled.Edit;
                }
                else if (Operation == Operation.Delete)
                {
                    return Icons.Material.Filled.Delete;
                }

                return Icons.Material.Filled.Error;
            }
        }

        private async Task OnConfirmationAsync()
        {
            await OnConfirmation.InvokeAsync();
            _mudDialog.Close(DialogResult.Ok(true));
        }

        private void Cancel()
        {
            _mudDialog.Cancel();
        }
    }
}
