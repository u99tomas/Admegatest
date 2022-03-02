using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AdMegasoft.Web.Shared.Components.Foundation
{
    public partial class CheckIcon
    {
        [Parameter]
        public bool Checked { get; set; }

        private string _icon
        {
            get
            {
                return Checked ? Icons.Material.Filled.CheckCircle : Icons.Material.Filled.Cancel;
            }
        }

        private Color _color
        {
            get
            {
                return Checked ? Color.Success : Color.Error;
            }
        }
    }
}
