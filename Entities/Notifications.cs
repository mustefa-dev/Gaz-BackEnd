using BackEndStructuer.Entities;

namespace Gaz_BackEnd.Entities
{
    

    public class Notifications : BaseEntity<Guid>
    { 

        public Guid? NotifyId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public string? Picture { get; set; }
        public string? NotifyFor { get; set; }
        public DateTime? Date { get; set; }
        public Guid? UserId { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; }
    }
}