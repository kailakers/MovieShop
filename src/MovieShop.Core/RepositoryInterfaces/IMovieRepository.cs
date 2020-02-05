using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Core.Entities;

namespace MovieShop.Core.RepositoryInterfaces
{
   public interface IMovieRepository: IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetMovies(int pageSize = 20, int pageIndex = 0, string title = "");
        Task<IEnumerable<Movie>> GetTopRatedMovies();
        Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId);
        Task<IEnumerable<Movie>> GetHighestGrossingMovies();
    }
}
