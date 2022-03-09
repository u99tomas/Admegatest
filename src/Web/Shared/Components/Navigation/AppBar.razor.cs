using Microsoft.AspNetCore.Components;

namespace Web.Shared.Components.Navigation
{
    public partial class AppBar
    {
        [Parameter]
        public EventCallback OnClickMenu { get; set; }

        private async void ClickMenu()
        {
            await OnClickMenu.InvokeAsync();
        }
    }
}
