using Domain.Common;

namespace Domain.Entities
{
    public class User : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool Enabled { get; set; }
    }
}
