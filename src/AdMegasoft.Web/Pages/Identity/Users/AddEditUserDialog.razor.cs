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
        public AddEditUserCommand AddEditUserCommand { get; set; } = new();

        private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

        private List<GetAllRolesResponse> _roles { get; set; } = new();
        private IEnumerable<int> _selectedRoles { get; set; } = new HashSet<int>();

        protected override async Task<Task> OnInitializedAsync()
        {
            await GetRoles();
            await GetSelectedRoles();
            return base.OnInitializedAsync();
        }

        private async Task GetSelectedRoles()
        {
            if (AddEditUserCommand.Id != 0)
            {
                var result = await _mediator.Send(new GetRolesIdsOfUserQuery { UserId = AddEditUserCommand.Id });
                _selectedRoles = result.Data;
            }
        }

        private async Task GetRoles()
        {
            var result = await _mediator.Send(new GetAllRolesQuery());
            _roles = result.Data;
        }

        private async void Submit()
        {
            AddEditUserCommand.RoleIds = _selectedRoles;
            AddEditUserCommand.IsActive = true;

            var result = await _mediator.Send(AddEditUserCommand);
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

        private void TogglePasswordVisibility()
        {
            if (_passwordVisibility)
            {
                _passwordVisibility = false;
                _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
                _passwordInput = InputType.Password;
            }
            else
            {
                _passwordVisibility = true;
                _passwordInputIcon = Icons.Material.Filled.Visibility;
                _passwordInput = InputType.Text;
            }
        }
    }
}
