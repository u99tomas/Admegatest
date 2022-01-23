using AdMegasoft.Core.Common;

namespace AdMegasoft.Core.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
