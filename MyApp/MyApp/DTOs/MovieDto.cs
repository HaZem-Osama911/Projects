using MyApp.Models.APIModels;

namespace MyApp.DTOs
{
    public class MovieDto
    {
        [MaxLength(250)]
        public string Title { get; set; } = string.Empty;

        public int Year { get; set; }
        public double Rate { get; set; }

        [MaxLength(2500)]
        public string StoreLuine { get; set; } = string.Empty;

        public string Poster { get; set; } = string.Empty;

        public int GenreId { get; set; }
    }

}
