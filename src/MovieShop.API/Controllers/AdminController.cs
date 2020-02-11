using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ApiModels.Request;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;
        public AdminController(IMovieService movieService, IUserService userService)
        {
            _movieService = movieService;
            _userService = userService;
           
        }

        [HttpPost("movie")]
        public async Task<IActionResult> CreateMovie([FromBody] MovieCreateRequest movieCreateRequest)
        {
            var createdMovie = await _movieService.CreateMovie(movieCreateRequest);
            return Ok(createdMovie);
        }
    }
}