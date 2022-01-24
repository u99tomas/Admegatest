using AdMegasoft.Domain.Common;

namespace AdMegasoft.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
