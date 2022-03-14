using Application.Features.Companies.Commands;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Web.Infrastructure.Extensions;

namespace Web.Pages.Admin.Companies
{
    public partial class AddEditCompanyDialog
    {
        [CascadingParameter]
        private MudDialogInstance _mudDialog { get; set; }

        [Parameter]
        public AddEditCompanyCommand Model { get; set; } = new();

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
