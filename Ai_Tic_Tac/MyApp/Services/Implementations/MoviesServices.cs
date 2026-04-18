using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models.APIModels;
using MyApp.Services.Interfaces;

namespace MyApp.Services.Implementations
{
    public class MoviesServices : IMoviesServices
    {
        private readonly ApplicationDbContext _context;
        public MoviesServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetAll()
        {
            return await _context.Movies.Include(m => m.Genre).ToListAsync();
        }

        public async Task<Movie?> GetById(int id)
        {
            return await _context.Movies
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Movie> Create(MovieDto dto)
        {
            var movie = new Movie
            {
                Title = dto.Title,
                Year = dto.Year,
                Rate = dto.Rate,
                StoreLuine = dto.StoreLuine,
                Poster = dto.Poster,
                GenreId = dto.GenreId
            };

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        public async Task<Movie?> Update(int id, MovieDto dto)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return null;

            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Rate = dto.Rate;
            movie.StoreLuine = dto.StoreLuine;
            movie.Poster = dto.Poster;
            movie.GenreId = dto.GenreId;

            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        public async Task<bool> Delete(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return false;

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
