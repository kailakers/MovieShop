using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MovieShop.Core.ApiModels.Response;
using MovieShop.Core.Entities;
using MovieShop.Core.Exceptions;
using MovieShop.Core.Helpers;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Core.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }


        public async Task<PagedResultSet<MovieResponseModel>> GetMoviesByPagination(
            int pageSize = 20, int pageIndex = 0, string title = "")
        {
            Expression<Func<Movie, bool>> filterExpression = null;
            if (!string.IsNullOrEmpty(title)) filterExpression = movie => title != null && movie.Title.Contains(title);

            var pagedMovies = await _movieRepository.GetPagedData(pageIndex, pageSize,
                                                                  movies => movies.OrderBy(m => m.Title),
                                                                  filterExpression);
            var movies =
                new PagedResultSet<MovieResponseModel>(_mapper.Map<List<MovieResponseModel>>(pagedMovies),
                                                           pagedMovies.PageIndex,
                                                           pagedMovies.PageIndex, pagedMovies.TotalCount);
            return movies;
        }

        public async Task<MovieDetailsResponseModel> GetMovieAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null) throw new NotFoundException("Movie", id);
            var response = _mapper.Map<MovieDetailsResponseModel>(movie);
            return response;
        }


        public async Task<int> GetMoviesCount(string title = "")
        {
            if (string.IsNullOrEmpty(title)) return await _movieRepository.GetCountAsync();
            return await _movieRepository.GetCountAsync(m => m.Title.Contains(title));
        }

        public async Task<IEnumerable<MovieResponseModel>> GetTopRatedMovies()
        {
            var topMovies = await _movieRepository.GetTopRatedMovies();
            var response = _mapper.Map<IEnumerable<MovieResponseModel>>(topMovies);
            return response;
        }

        public async Task<IEnumerable<MovieResponseModel>> GetHighestGrossingMovies()
        {
            var movies = await _movieRepository.GetHighestGrossingMovies();
            var response = _mapper.Map<IEnumerable<MovieResponseModel>>(movies);
            return response;
        }

        public async Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieRepository.GetMoviesByGenre(genreId);
            if (!movies.Any()) throw new NotFoundException("Movies for genre", genreId);
            var response = _mapper.Map<IEnumerable<MovieResponseModel>>(movies);
            return response;
        }
    }
}