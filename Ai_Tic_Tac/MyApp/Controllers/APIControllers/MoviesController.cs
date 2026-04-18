using Microsoft.AspNetCore.Mvc;
using MyApp.Models.APIModels;
using MyApp.Services.Interfaces;

namespace MyApp.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesServices _moviesServices;
        public MoviesController(IMoviesServices moviesServices)
        {
            _moviesServices = moviesServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _moviesServices.GetAll();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movie = await _moviesServices.GetById(id);
            if (movie == null) return NotFound($"Movie with ID {id} not found.");
            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] MovieDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var movie = await _moviesServices.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] MovieDto dto)
        {
            var movie = await _moviesServices.Update(id, dto);
            if (movie == null) return NotFound($"Movie with ID {id} not found.");
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _moviesServices.Delete(id);
            if (!success) return NotFound($"Movie with ID {id} not found.");
            return Ok("Movie Deleted Successfully");
        }
    }
}
