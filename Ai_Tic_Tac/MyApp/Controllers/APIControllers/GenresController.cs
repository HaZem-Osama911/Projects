using Microsoft.AspNetCore.Mvc;
using MyApp.Services.Interfaces;
using MyApp.Models.APIModels;

namespace MyApp.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresServices _genresServices;
        public GenresController(IGenresServices genresServices)
        {
            _genresServices = genresServices;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genresServices.GetAll();
            return Ok(genres);
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var genre = await _genresServices.GetById(id);
            if (genre == null) return NotFound($"Genre with ID {id} not found.");
            return Ok(genre);
        }

        // POST: api/Genres
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] GenreDto dto)
        {
            if (dto == null) return BadRequest("Genre data is null.");

            var genre = await _genresServices.Create(dto);

            return CreatedAtAction(nameof(GetById), new { id = genre.Id }, genre);
        }

        // PUT: api/Genres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] GenreDto dto)
        {
            var genre = await _genresServices.Update(id, dto);
            if (genre == null) return NotFound($"Genre with ID {id} not found.");
            return Ok(genre);
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _genresServices.Delete(id);
            if (!success) return NotFound($"Genre with ID {id} not found.");
            return Ok("Genre Deleted Successfully");
        }
    }
}
