using Microsoft.AspNetCore.Components;

namespace Web.Shared.Components.Card
{
    public partial class PersonCard
    {
        [Parameter] 
        public string Class { get; set; }
    }
}
