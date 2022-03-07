using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Web.Shared.Components.Navigation
{
    public partial class RedirectTo
    {
        [CascadingParameter]
        private Task<AuthenticationState> _stateTask { get; set; }

        [Parameter]
        public string Url { get; set; }

        [Parameter]
        public bool NeedAuthentication { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var user = (await _stateTask).User;

            if (NeedAuthentication)
            {
                if (user.Identity.IsAuthenticated)
                {
                    _navigationManager.NavigateTo(Url);
                }
            }
            else
            {
                _navigationManager.NavigateTo(Url);
            }

        }
    }
}
