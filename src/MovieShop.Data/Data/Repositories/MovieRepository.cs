using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;

namespace MovieShop.Infrastructure.Data.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> GetMovies(int pageSize = 20, int pageIndex = 0, string title = "")
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetTopRatedMovies()
        {
            var topRatedMovies = await _dbContext.Reviews.Include(m => m.Movie)
                                                 .GroupBy(r => new
                                                               {
                                                                   Id = r.MovieId,
                                                                   r.Movie.PosterUrl,
                                                                   r.Movie.Title,
                                                                   r.Movie.BackdropUrl
                                                               })
                                                 .OrderByDescending(g => g.Average(m => m.Rating))
                                                 .Select(m => new Movie
                                                              {
                                                                  Id = m.Key.Id,
                                                                  PosterUrl = m.Key.PosterUrl,
                                                                  Title = m.Key.Title,
                                                                  BackdropUrl = m.Key.BackdropUrl,
                                                                  Rating = m.Average(x => x.Rating)
                                                              })
                                                 .Take(25)
                                                 .ToListAsync();

            return topRatedMovies;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId)
        {
            throw new NotImplementedException();
        }
    }
}