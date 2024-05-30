using BackEndStructuer.Entities;

namespace Gaz_BackEnd.Entities
{
    public class Document: BaseEntity<Guid>
    {
        public Guid? FileID { get; set; }
        public File? File { get; set; }
        public Guid? ProviderId { get; set; }
        public Provider? Provider { get; set; }
    }
}
