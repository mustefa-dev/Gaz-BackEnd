using BackEndStructuer.Entities;
using System.ComponentModel.DataAnnotations;

namespace Gaz_BackEnd.Entities
{
    public class Governorate: BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public ICollection<District>? Districts { get; set; }
        public ICollection<Address>? Addresses { get; set; }
        public ICollection<Station>? Stations { get; set; }
    }
}
