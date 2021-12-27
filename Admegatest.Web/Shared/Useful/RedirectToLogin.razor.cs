using Microsoft.AspNetCore.Components;

namespace Admegatest.Web.Components.AuthenticationAndAuthorization
{
    public partial class RedirectToLogin
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        protected override void OnInitialized()
        {
            _navigationManager.NavigateTo($"/login?returnUrl={Uri.EscapeDataString(_navigationManager.Uri)}");
        }
    }
}
