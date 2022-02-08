using Application.Features.Roles.Commands.Add;
using Application.Validators;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AdMegasoft.Web.Pages.Identity.Roles
{
    public partial class AddEditRoleDialog
    {
        [Inject]
        private ISnackbar _snackbar { get; set; }
        [Inject]
        private IMediator _mediator { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public AddEditRoleCommand AddEditRoleCommand { get; set; } = new();
        private MudForm _form;
        private AddEditRoleCommandValidator _validator = new();

        private async void SaveAsync()
        {
            _form.Validate();

            if (!_form.IsValid)
            {
                return;
            }

            var response = await _mediator.Send(AddEditRoleCommand);

            MudDialog.Close();

            if (AddEditRoleCommand.Id == 0)
            {
                _snackbar.Add($"Se ha creado el Rol {AddEditRoleCommand.Name}", Severity.Success);
            }
            else
            {
                _snackbar.Add($"Se ha actualizado el Rol {AddEditRoleCommand.Name}", Severity.Success);
            }
            
        }

    }
}
