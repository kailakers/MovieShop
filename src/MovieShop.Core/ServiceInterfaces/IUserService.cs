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
        Task<User> GetUser(string username);

        Task AddFavorite(FavoriteRequestModel favorite);
        Task RemoveFavorite(FavoriteRequestModel favorite);
        Task<bool> FavoriteExists(FavoriteRequestModel favorite);

        Task PurchaseMovie(PurchaseRequestModel purchaseRequest);
        Task<bool> IsMoviePurchased(int movieId, int userId);
    }
}