using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Linq.Expressions;

namespace AdMegasoft.Web.Shared.Components.Field
{
    public partial class PasswordField
    {
        [Parameter]
        public bool Immediate { get; set; }

        [Parameter]
        public string Class { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public Expression<Func<string>> For { get; set; }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public string Value
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = value;
                ValueChanged.InvokeAsync(value);
            }
        }

        private string _value;

        private bool _visibility;

        private InputType _type = InputType.Password;

        private string _icon = Icons.Material.Filled.VisibilityOff;

        private void TogglePasswordVisibility()
        {
            if (_visibility)
            {
                _visibility = false;
                _icon = Icons.Material.Filled.VisibilityOff;
                _type = InputType.Password;
            }
            else
            {
                _visibility = true;
                _icon = Icons.Material.Filled.Visibility;
                _type = InputType.Text;
            }
        }
    }
}
