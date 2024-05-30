using BackEndStructuer.DATA.DTOs;

namespace Gaz_BackEnd.DATA.DTOs.Distric
{
    public class DistrictFilter:BaseFilter
    {
        public string? Name { get; set; }
        public Guid? GovernorateId { get; set; }
    }
}
