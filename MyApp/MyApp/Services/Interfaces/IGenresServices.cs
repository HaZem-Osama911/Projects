using MyApp.Models.APIModels;

namespace MyApp.Services.Interfaces
{
    public interface IGenresServices
    {
        Task<List<Genre>> GetAll();
        Task<Genre?> GetById(int id);
        Task<Genre> Create(GenreDto dto);
        Task<Genre?> Update(int id, GenreDto dto);
        Task<bool> Delete(int id);
    }
}
