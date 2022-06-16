using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Contract>? Contracts { get; set; }

        public Address? Address { get; set; }

        [ForeignKey("Address")]
        public int? AddrId { get; set; }
    }
}