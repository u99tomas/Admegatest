using Domain.Common;

namespace Domain.Entities
{
    public class Company : IEntity<int>
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Denomination { get; set; }
    }
}
