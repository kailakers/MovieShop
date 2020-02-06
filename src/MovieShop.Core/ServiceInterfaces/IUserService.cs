using System.Threading.Tasks;
using MovieShop.Core.ApiModels.Request;
using MovieShop.Core.ApiModels.Response;
using MovieShop.Core.Entities;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface IUserService
    {
        Task<bool> ValidateUser(string username, string password);
        Task<UserRegisterResponseModel> CreateUser(UserRegisterRequestModel requestModel);
        Task<User> GetUserDetails(int id);
        Task<User> GetUser(string username);
    }
}