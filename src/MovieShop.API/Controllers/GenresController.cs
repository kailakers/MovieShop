using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.API.Caching;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ICachedGenreService _genreService;
        public GenresController(ICachedGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenres();
            return Ok(genres);
        }

    }
}