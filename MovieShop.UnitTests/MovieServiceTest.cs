using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using MovieShop.Core.ApiModels.Response;
using MovieShop.Core.Entities;
using MovieShop.Core.MappingProfiles;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.Services;
using NUnit.Framework;

namespace MovieShop.UnitTests
{
    [TestFixture]
    public class MovieServiceTest
    {
        [SetUp]
        public void SetUp()
        {
            _mockMovieRepository.Setup(m => m.GetHighestGrossingMovies()).ReturnsAsync(GetAllMockMovies);
        }

        private readonly Mock<IMovieRepository> _mockMovieRepository;
        private readonly Mock<IAsyncRepository<MovieGenre>> _mockGenresRepository;
        private readonly Mock<IPurchaseRepository> _mockPurchaseRepository;
        private readonly Mock<IAsyncRepository<Favorite>> _mockFavoriteRepository;
        private readonly Mapper _mapper;

        public MovieServiceTest()
        {
            var myProfile = new MoviesMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);

            _mockMovieRepository = new Mock<IMovieRepository>();
            _mockGenresRepository = new Mock<IAsyncRepository<MovieGenre>>();
            _mockPurchaseRepository = new Mock<IPurchaseRepository>();
            _mockFavoriteRepository = new Mock<IAsyncRepository<Favorite>>();
        }

        private static IEnumerable<Movie> GetAllMockMovies()
        {
            return new List<Movie>
                   {
                       new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
                       new Movie {Id = 2, Title = "Avatar", Budget = 1200000},
                       new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
                       new Movie {Id = 4, Title = "Titanic", Budget = 1200000},
                       new Movie {Id = 5, Title = "The Avengers", Budget = 1200000},
                       new Movie {Id = 6, Title = "Furious 7", Budget = 1200000}
                   };
        }

        [Test]
        public async Task TestListOfMoviesFromFakeData()
        {
            var sut = new MovieService(_mockMovieRepository.Object, _mapper, _mockGenresRepository.Object,
                                       _mockPurchaseRepository.Object,
                                       _mockFavoriteRepository.Object);
            var movies = await sut.GetHighestGrossingMovies();

            Assert.NotNull(movies);
            Assert.That(movies.Count(), Is.EqualTo(6));
            CollectionAssert.AllItemsAreInstancesOfType(movies, typeof(MovieResponseModel));
        }
    }
}