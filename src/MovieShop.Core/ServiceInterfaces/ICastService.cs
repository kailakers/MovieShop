using System.Threading.Tasks;
using MovieShop.Core.ApiModels.Response;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface ICastService
    {
        Task<CastDetailsResponseModel> GetCastDetailsWithMovies(int id);
    }
}