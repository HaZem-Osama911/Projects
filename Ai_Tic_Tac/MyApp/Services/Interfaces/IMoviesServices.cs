using MyApp.Models.APIModels;

namespace MyApp.Services.Interfaces
{
    public interface IMoviesServices
    {
        Task<List<Movie>> GetAll();
        Task<Movie?> GetById(int id);
        Task<Movie> Create(MovieDto dto);
        Task<Movie?> Update(int id, MovieDto dto);
        Task<bool> Delete(int id);
    }
}
