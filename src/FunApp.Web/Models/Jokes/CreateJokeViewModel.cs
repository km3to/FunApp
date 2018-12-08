using System.ComponentModel.DataAnnotations;

namespace FunApp.Web.Models.Jokes
{
    public class CreateJokeViewModel
    {
        [Required]
        [MinLength(20)]
        public string Content { get; set; }

        [ValidCategoryId]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}