using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ApiModels.Request;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;

        public UserController( IUserService userService, IMovieService movieService)
        {
            _userService = userService;
            _movieService = movieService;
        }

        [Authorize]
        [HttpPost("purchase")]
        public async Task<ActionResult> CreatePurchase([FromBody] PurchaseRequestModel purchaseRequest)
        {
            await _userService.PurchaseMovie(purchaseRequest);
            return Ok();
        }

        [Authorize]
        [HttpPost("favorite")]
        public async Task<ActionResult> Favorite([FromBody] FavoriteRequestModel favoriteRequest)
        {
            await _userService.AddFavorite(favoriteRequest);
            return Ok();
        }

        [Authorize]
        [HttpGet("{id:int}/purchases")]
        public async Task<ActionResult> GetUserPurchasedMoviesAsync(int id)
        {
            var userMovies = await _userService.GetAllPurchasesForUser(id);
            return Ok(userMovies);
        }

        [Authorize]
        [HttpGet("{id:int}/favorites")]
        public async Task<ActionResult> GetUserFavoriteMoviesAsync(int id)
        {
            var userMovies = await _userService.GetAllFavoritesForUser(id);
            return Ok(userMovies);
        }
    }
}