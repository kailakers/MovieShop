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
                                                 .GroupBy(m => m.MovieId)
                                                 .OrderByDescending(r => r.Average(m => m.Rating))
                                                 .Take(25)
                                                 .Select(m => new Movie
                                                              {
                                                                  Id = m.Key,
                                                                  PosterUrl = m.FirstOrDefault().Movie.PosterUrl,
                                                                  Title = m.FirstOrDefault().Movie.Title,
                                                                  BackdropUrl = m.FirstOrDefault().Movie.BackdropUrl,
                                                                  Rating = m.Average( x => x.Rating)
                                                                
                                                              }).ToListAsync();

            return topRatedMovies;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId)
        {
            throw new NotImplementedException();
        }
    }
}