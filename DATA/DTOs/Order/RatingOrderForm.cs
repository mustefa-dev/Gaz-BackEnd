using System.ComponentModel.DataAnnotations;

namespace Gaz_BackEnd.DATA.DTOs.Order
{
    public class RatingOrderForm
    {
        [Required]
        public double Rating { get; set; }
    }
}
