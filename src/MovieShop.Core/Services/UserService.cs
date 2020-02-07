using System;
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
    public class UserService: IUserService
    {
        private readonly ICryptoService _encryptionService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(ICryptoService encryptionService, IUserRepository userRepository, IMapper mapper)
        {
            _encryptionService = encryptionService;
            _userRepository = userRepository;
            _mapper = mapper;
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
            if (dbUser != null && string.Equals(dbUser.Email, requestModel.Email, StringComparison.CurrentCultureIgnoreCase))
                throw new EmailExistsException("Email Already Exits");
            var salt = _encryptionService.CreateSalt();
            var hashedPassword = _encryptionService.HashPassword(requestModel.Password, salt);
            var user = new User { Email = requestModel.Email, Salt = salt, HashedPassword = hashedPassword, FirstName = requestModel.FirstName, LastName = requestModel.LastName };
            var createdUser = await _userRepository.AddAsync(user);

            var response = _mapper.Map<UserRegisterResponseModel>(createdUser);
            return response;
        }

        public async Task<UserRegisterResponseModel> GetUserDetails(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User", id);
            }

            var response = _mapper.Map<UserRegisterResponseModel>(user);
            return response;
        }

        public async Task<User> GetUser(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}