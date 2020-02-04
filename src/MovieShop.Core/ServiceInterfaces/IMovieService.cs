using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.ApiModels.Response;
using MovieShop.Core.Entities;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync(int pageSize = 20, int pageIndex = 0, string title = "");
        Task<Movie> GetMovieAsync(int id);
        Task<int> GetMoviesCount(string title = "");
        Task<IEnumerable<MovieCardResponseModel>> GetTopRatedMovies();
        Task<IEnumerable<Movie>> GetHighestGrossingMovies();
        Task<IEnumerable<MovieCardResponseModel>> GetMoviesByGenre(int genreId);
    }
}