using System.Threading.Tasks;
using MovieShop.Core.ApiModels.Request;
using MovieShop.Core.ApiModels.Response;
using MovieShop.Core.Entities;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Core.Services
{
    public class UserService: IUserService
    {
        private readonly ICryptoService _encryptionService;

        public UserService(ICryptoService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        public async Task<bool> ValidateUser(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserRegisterResponseModel> CreateUser(UserRegisterRequestModel requestModel)
        {
            var salt = _encryptionService.CreateSalt();
            var hashedPassword = _encryptionService.HashPassword(requestModel.Password, salt);
            return new UserRegisterResponseModel();
        }

        public async Task<User> GetUserDetails(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUser(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}