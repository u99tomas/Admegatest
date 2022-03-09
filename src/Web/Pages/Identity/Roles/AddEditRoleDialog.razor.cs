using Application.Features.Roles.Commands.Add;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Web.Infrastructure.Extensions;

namespace Web.Pages.Identity.Roles
{
    public partial class AddEditRoleDialog
    {
        [CascadingParameter]
        private MudDialogInstance _mudDialog { get; set; }

        [Parameter]
        public AddEditRoleCommand Model { get; set; } = new();

        private bool EditMode { get => Model.Id != 0; }

        private async void SubmitAsync()
        {
            var result = await _mediator.Send(Model);

            if (result.Succeeded)
            {
                _mudDialog.Close();
            }

            _snackBar.ShowMessage(result);
        }
    }
}
