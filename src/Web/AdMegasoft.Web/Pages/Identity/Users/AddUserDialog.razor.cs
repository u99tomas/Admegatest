using Application.Features.Roles.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Components;
using System.Linq;

namespace AdMegasoft.Web.Pages.Identity.Users
{
    public partial class AddUserDialog
    {
        [Inject]
        private IMediator _mediator { get; set; }

        private List<GetAllRolesResponse> _roles { get; set; } = new();
        private IEnumerable<int> _selectedRoleIds { get; set; } = new HashSet<int>();

        protected override async Task<Task> OnInitializedAsync()
        {
            _roles = await _mediator.Send(new GetAllRolesQuery());

            return base.OnInitializedAsync();
        }

        private async void SaveAsync()
        {

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
