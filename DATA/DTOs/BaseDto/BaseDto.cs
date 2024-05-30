using System.ComponentModel.DataAnnotations;

namespace Gaz_BackEnd.DATA.DTOs.BaseDto;

public class BaseDto<TId>
{
    [Key]
    public TId Id { get; set; }

    public bool Deleted { get; set; } = false;
    public DateTime? CreationDate { get; set; } = DateTime.UtcNow;
}