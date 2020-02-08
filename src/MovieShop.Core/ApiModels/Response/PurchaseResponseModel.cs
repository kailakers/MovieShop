using System.Collections.Generic;

namespace MovieShop.Core.ApiModels.Response
{
    public class PurchaseResponseModel
    {
        public int UserId { get; set; }

        public List<MovieResponseModel> PurchasedMovies { get; set; }
    }
}