using Application.Features.Roles.Commands.Add;
using Application.Validators;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AdMegasoft.Web.Pages.Identity.Roles
{
    public partial class AddRoleDialog
    {
        [Inject]
        private ISnackbar _snackbar { get; set; }
        [Inject]
        private IMediator _mediator { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        private MudForm _form;
        private AddRoleCommand _model = new();
        private AddRoleCommandValidator _validator = new();

        private async void SaveAsync()
        {
            _form.Validate();

            if (!_form.IsValid)
            {
                return;
            }

            var response = await _mediator.Send(_model);

            MudDialog.Close(response);

            _snackbar.Add($"Se ha creado el Rol {_model.Name}", Severity.Success);
        }

    }
}
