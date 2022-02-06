using Application.Features.Roles.Commands.Add;
using Application.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AdMegasoft.Web.Pages.Identity.Roles
{
    public partial class AddRoleDialog
    {
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        private MudForm _form;
        private AddRoleCommand _model = new();
        private AddRoleCommandValidator _validator = new();


        private void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}
