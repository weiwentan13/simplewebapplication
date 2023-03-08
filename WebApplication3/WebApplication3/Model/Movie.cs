using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Model
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Year { get; set; }
        public string Summary { get; set; }
    }
}
