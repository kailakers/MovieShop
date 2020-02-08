using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.ApiModels.Request;
using MovieShop.Core.Entities;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface IPurchaseService
    {
        Task PurchaseMovie(PurchaseRequestModel purchaseRequest);
        Task<bool> IsMoviePurchased(int movieId, int userId);
       
    }
}