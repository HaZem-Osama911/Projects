using MyApp.Services.Interfaces;
using MyApp.Data;
using Microsoft.EntityFrameworkCore;
using MyApp.Models.APIModels;

namespace MyApp.Services.Implementations
{
    public class GenresServices : IGenresServices
    {
        private readonly ApplicationDbContext _context;
        public GenresServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> GetAll()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre?> GetById(int id)
        {
            return await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Genre> Create(GenreDto dto)
        {
            var genre = new Genre
            {
                Name = dto.Name
            };

            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();

            return genre;
        }

        public async Task<Genre?> Update(int id, GenreDto dto)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return null;

            genre.Name = dto.Name;

            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();

            return genre;
        }

        public async Task<bool> Delete(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return false;

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
