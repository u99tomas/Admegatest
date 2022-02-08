using Application.Features.Roles.Queries.GetAll;
using Application.Features.Users.Commands.AddEdit;
using Application.Validators;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AdMegasoft.Web.Pages.Identity.Users
{
    public partial class AddEditUserDialog
    {
        #region Injections
        [Inject]
        private ISnackbar _snackbar { get; set; }
        [Inject]
        private IMediator _mediator { get; set; }
        #endregion

        #region Parameters
        [CascadingParameter]
        private MudDialogInstance _mudDialog { get; set; }
        [Parameter]
        public AddEditUserCommand AddEditUserCommand { get; set; } = new();
        #endregion

        #region Password field behavior
        private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
        #endregion

        #region Form 
        private MudForm _form;
        private AddEditUserCommandValidator _validator { get; set; } = new();
        private List<GetAllRolesResponse> _roles { get; set; } = new();
        private IEnumerable<int> _selectedRoleIds { get; set; } = new HashSet<int>();
        #endregion

        protected override async Task<Task> OnInitializedAsync()
        {
            _roles = await _mediator.Send(new GetAllRolesQuery());

            return base.OnInitializedAsync();
        }

        private async void SaveAsync()
        {
            await _form.Validate();

            if (!_form.IsValid)
            {
                return;
            }

            AddEditUserCommand.RoleIds = _selectedRoleIds;
            AddEditUserCommand.IsActive = true;

            var response = await _mediator.Send(AddEditUserCommand);

            _mudDialog.Close();

            if (AddEditUserCommand.Id == 0)
            {
                _snackbar.Add($"Se ha creado el Usuario {AddEditUserCommand.AccountName}", Severity.Success);
            }
            else
            {
                _snackbar.Add($"Se ha actualizado el Usuario {AddEditUserCommand.AccountName}", Severity.Success);
            }
        }

        private string GetMultiSelectionTextForRoles(List<string> selectedValues)
        {
            var nameOfSelectedRoles = _roles
                .Where(r => selectedValues.Contains(r.Id.ToString()))
                .Select(r => r.Name);

            return string.Join(", ", nameOfSelectedRoles.ToArray());
        }

        #region (Methods) Password field behavior
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
        #endregion
    }
}
