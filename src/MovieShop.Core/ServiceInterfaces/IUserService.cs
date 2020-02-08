using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.ApiModels.Request;
using MovieShop.Core.ApiModels.Response;
using MovieShop.Core.Entities;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface IUserService
    {
        Task<User> ValidateUser(string email, string password);
        Task<UserRegisterResponseModel> CreateUser(UserRegisterRequestModel requestModel);
        Task<UserRegisterResponseModel> GetUserDetails(int id);
        Task<User> GetUser(string email);

        Task AddFavorite(FavoriteRequestModel favoriteRequest);
        Task RemoveFavorite(FavoriteRequestModel favoriteRequest);
        Task<bool> FavoriteExists(FavoriteRequestModel favoriteRequest);

        Task PurchaseMovie(PurchaseRequestModel purchaseRequest);
        Task<bool> IsMoviePurchased(int movieId, int userId);
        Task<IEnumerable<Purchase>> GetAllPurchases();

    }
}