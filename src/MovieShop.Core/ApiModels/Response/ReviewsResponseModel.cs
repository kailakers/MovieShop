using System.Collections.Generic;

namespace MovieShop.Core.ApiModels.Response
{
    public class ReviewsResponseModel
    {
        public int UserId { get; set; }
        public List<ReviewMovieResponseModel> movieReviews { get; set; }

       
    }
    public class ReviewMovieResponseModel
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string ReviewText { get; set; }
        public decimal Rating { get; set; }
    }
}