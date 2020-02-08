using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MovieShop.Core.ApiModels.Request;
using MovieShop.Core.ApiModels.Response;
using MovieShop.Core.Entities;
using MovieShop.Core.Exceptions;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ICryptoService _encryptionService;
        private readonly IAsyncRepository<Favorite> _favoriteRepository;
        private readonly IMapper _mapper;
        private readonly IMovieService _movieService;
        private readonly IAsyncRepository<Purchase> _purchaseRepository;
        private readonly IUserRepository _userRepository;

        public UserService(ICryptoService encryptionService, IUserRepository userRepository, IMapper mapper,
                           IAsyncRepository<Favorite> favoriteRepository, ICurrentUserService currentUserService,
                           IMovieService movieService, IAsyncRepository<Purchase> purchaseRepository)
        {
            _encryptionService = encryptionService;
            _userRepository = userRepository;
            _mapper = mapper;
            _favoriteRepository = favoriteRepository;
            _currentUserService = currentUserService;
            _movieService = movieService;
            _purchaseRepository = purchaseRepository;
        }

        public async Task<User> ValidateUser(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null) return null;
            var hashedPassword = _encryptionService.HashPassword(password, user.Salt);
            return user.HashedPassword != hashedPassword ? null : user;
        }

        public async Task<UserRegisterResponseModel> CreateUser(UserRegisterRequestModel requestModel)
        {
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser != null &&
                string.Equals(dbUser.Email, requestModel.Email, StringComparison.CurrentCultureIgnoreCase))
                throw new ConflictException("Email Already Exits");
            var salt = _encryptionService.CreateSalt();
            var hashedPassword = _encryptionService.HashPassword(requestModel.Password, salt);
            var user = new User
                       {
                           Email = requestModel.Email,
                           Salt = salt,
                           HashedPassword = hashedPassword,
                           FirstName = requestModel.FirstName,
                           LastName = requestModel.LastName
                       };
            var createdUser = await _userRepository.AddAsync(user);

            var response = _mapper.Map<UserRegisterResponseModel>(createdUser);
            return response;
        }

        public async Task<UserRegisterResponseModel> GetUserDetails(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new NotFoundException("User", id);

            var response = _mapper.Map<UserRegisterResponseModel>(user);
            return response;
        }

        public async Task<User> GetUser(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            if (_currentUserService.UserId != favoriteRequest.UserId)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to purchase");
            // See if Movie is already Favorited.
            if (await FavoriteExists(favoriteRequest)) throw new ConflictException("Movie already Favorited");

            var favorite = _mapper.Map<Favorite>(favoriteRequest);
            await _favoriteRepository.AddAsync(favorite);
        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            var favorite = _mapper.Map<Favorite>(favoriteRequest);
            await _favoriteRepository.DeleteAsync(favorite);
        }

        public async Task<bool> FavoriteExists(FavoriteRequestModel favorite)
        {
            return await _favoriteRepository.GetExistsAsync(f => f.MovieId == favorite.MovieId &&
                                                                 f.UserId == favorite.UserId);
        }

        public async Task PurchaseMovie(PurchaseRequestModel purchaseRequest)
        {
            if (_currentUserService.UserId != purchaseRequest.UserId)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to purchase");

            // See if Movie is already purchased.
            if (await IsMoviePurchased(purchaseRequest.MovieId, purchaseRequest.UserId))
                throw new ConflictException("Movie already Purchased");
            // Get Movie Price from Movie Table
            var movie = await _movieService.GetMovieAsync(purchaseRequest.MovieId);
            purchaseRequest.TotalPrice = movie.Price;

            var purchase = _mapper.Map<Purchase>(purchaseRequest);
            await _purchaseRepository.AddAsync(purchase);
        }

        public async Task<bool> IsMoviePurchased(int movieId, int userId)
        {
            return await _purchaseRepository.GetExistsAsync(p => p.UserId == userId && p.MovieId == movieId);
        }

        public async Task<PurchaseResponseModel> GetAllPurchases()
        {
            var purchasedMovies = await _purchaseRepository.ListAllWithIncludesAsync(p => p.UserId == _currentUserService.UserId,
                                                                      p => p.Movie);
            return _mapper.Map<PurchaseResponseModel>(purchasedMovies);

        }
    }
}