using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.ApiModels.Response;
using MovieShop.Core.Entities;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieCardResponseModel>> GetMoviesByPagination(int pageSize = 20, int pageIndex = 0, string title = "");
        Task<MovieDetailsResponseModel> GetMovieAsync(int id);
        Task<int> GetMoviesCount(string title = "");
        Task<IEnumerable<MovieCardResponseModel>> GetTopRatedMovies();
        Task<IEnumerable<MovieCardResponseModel>> GetHighestGrossingMovies();
        Task<IEnumerable<MovieCardResponseModel>> GetMoviesByGenre(int genreId);
    }
}