using System.Collections.Generic;
using AutoMapper;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.Infrastructure.Services;
using NUnit.Framework;

namespace MovieShop.UnitTests
{
    public class UserServiceTest
    {
        private ICurrentUserService _currentUserService;
        private ICryptoService _encryptionService;
        private IAsyncRepository<Favorite> _favoriteRepository;
        private IMapper _mapper;
        private List<Movie> _movies;
        private IMovieService _movieService;
        private IAsyncRepository<Purchase> _purchaseRepository;
        private IAsyncRepository<Review> _reviewRepository;
        private UserService _sut;
        private IUserRepository _userRepository;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _movies = new List<Movie>
                      {
                          new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
                          new Movie {Id = 2, Title = "Avatar", Budget = 1200000},
                          new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
                          new Movie {Id = 4, Title = "Titanic", Budget = 1200000},
                          new Movie {Id = 5, Title = "Inception", Budget = 1200000},
                          new Movie {Id = 6, Title = "Avengers: Age of Ultron", Budget = 1200000},
                          new Movie {Id = 7, Title = "Interstellar", Budget = 1200000},
                          new Movie {Id = 8, Title = "Fight Club", Budget = 1200000},
                          new Movie
                          {
                              Id = 9, Title = "The Lord of the Rings: The Fellowship of the Ring", Budget = 1200000
                          },
                          new Movie {Id = 10, Title = "The Dark Knight", Budget = 1200000},
                          new Movie {Id = 11, Title = "The Hunger Games", Budget = 1200000},
                          new Movie {Id = 12, Title = "Django Unchained", Budget = 1200000},
                          new Movie
                          {
                              Id = 13, Title = "The Lord of the Rings: The Return of the King", Budget = 1200000
                          },
                          new Movie {Id = 14, Title = "Harry Potter and the Philosopher's Stone", Budget = 1200000},
                          new Movie {Id = 15, Title = "Iron Man", Budget = 1200000},
                          new Movie {Id = 16, Title = "Furious 7", Budget = 1200000}
                      };
        }
    }
}