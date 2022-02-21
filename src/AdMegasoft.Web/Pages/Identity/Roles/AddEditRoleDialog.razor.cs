using AdMegasoft.Web.Extensions;
using Application.Features.Roles.Commands.Add;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AdMegasoft.Web.Pages.Identity.Roles
{
    public partial class AddEditRoleDialog
    {
        [CascadingParameter]
        private MudDialogInstance _mudDialog { get; set; }

        [Parameter]
        public AddEditRoleCommand Model { get; set; } = new();

        private bool EditMode { get => Model.Id != 0; }

        private async void Submit()
        {
            var result = await _mediator.Send(Model);

            _mudDialog.Close();

            _snackBar.ShowMessage(result);
        }
    }
}
