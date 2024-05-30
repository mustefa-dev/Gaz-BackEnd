using System.ComponentModel.DataAnnotations;

namespace BackEndStructuer.DATA.DTOs
{
    public class ArticleForm
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}