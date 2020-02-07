using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Core.Services
{
    public class GenreService: IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await _genreRepository.ListAllAsync();
        }
    }
}