namespace Web.Shared.Layouts
{
    public partial class MainLayout
    {
        public bool _drawerOpen = true;

        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        protected override void OnInitialized()
        {
            StateHasChanged();
        }
    }
}
