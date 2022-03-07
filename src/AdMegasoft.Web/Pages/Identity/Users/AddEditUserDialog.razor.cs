using AdMegasoft.Web.Extensions;
using Application.Features.Roles.Queries.GetAll;
using Application.Features.Roles.Queries.GetRolesIdsOfUser;
using Application.Features.Users.Commands.AddEdit;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AdMegasoft.Web.Pages.Identity.Users
{
    public partial class AddEditUserDialog
    {
        [CascadingParameter]
        private MudDialogInstance _mudDialog { get; set; }

        [Parameter]
        public AddEditUserCommand Model { get; set; } = new();

        private bool EditMode { get => Model.Id != 0; }

        private List<GetAllRolesResponse> _roles { get; set; } = new();

        protected override async Task<Task> OnInitializedAsync()
        {
            _roles = (await _mediator.Send(new GetAllRolesQuery())).Data;

            if (EditMode)
            {
                // Se deberia obtener el usuario completo? _meaditor.Send(GetUserById) <-- trae los roles
                Model.RoleIds = (await _mediator.Send(new GetRolesIdsOfUserQuery { UserId = Model.Id })).Data;
            }
            else
            {
                Model.RoleIds = new List<int>();
            }

            return base.OnInitializedAsync();
        }

        private async void SubmitAsync()
        {
            var result = await _mediator.Send(Model);

            _mudDialog.Close();

            _snackBar.ShowMessage(result);
        }

        private string GetMultiSelectionTextForRoles(List<string> selectedValues)
        {
            var nameOfSelectedRoles = _roles
                .Where(r => selectedValues.Contains(r.Id.ToString()))
                .Select(r => r.Name);

            return string.Join(", ", nameOfSelectedRoles.ToArray());
        }
    }
}
