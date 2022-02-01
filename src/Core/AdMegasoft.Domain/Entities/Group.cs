using AdMegasoft.Domain.Common;

namespace AdMegasoft.Domain.Entities
{
    public class Group : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
