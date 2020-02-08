using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using MovieShop.Core.ApiModels.Request;
using MovieShop.Core.Entities;
using MovieShop.Core.Exceptions;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Core.Services
{
    public class PurchaseService:IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly IMovieService _movieService;

        public PurchaseService(IPurchaseRepository purchaseRepository, IMapper mapper, ICurrentUserService currentUserService, IMovieService movieService)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _movieService = movieService;
        }

        public async Task PurchaseMovie(PurchaseRequestModel purchaseRequest)
        {
            if (_currentUserService.UserId != purchaseRequest.UserId)
            {
                throw new UnauthorizedAccessException("You are not Authorized to purchase");
            }

            // See if Movie is already purchased.
            if (await IsMoviePurchased(purchaseRequest.MovieId, purchaseRequest.UserId))
            {
                throw new ConflictException("Movie already Purchased");
            }
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
    }
}