using AdMegasoft.Web.Extensions;
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
        private MudDialogInstance _mudDialog { get; set; }

        [Parameter]
        public AddEditRoleCommand AddEditRoleCommand { get; set; } = new();

        private async void Submit()
        {
            var result = await _mediator.Send(AddEditRoleCommand);

            _mudDialog.Close();

            _snackbar.ShowMessage(result);
        }

    }
}
