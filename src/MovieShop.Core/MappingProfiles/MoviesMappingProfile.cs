using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieShop.Core.ApiModels.Request;
using MovieShop.Core.ApiModels.Response;
using MovieShop.Core.Entities;

namespace MovieShop.Core.MappingProfiles
{
    public class MoviesMappingProfile : Profile
    {
        public MoviesMappingProfile()
        {
            CreateMap<Movie, MovieResponseModel>();
            CreateMap<Cast, CastDetailsResponseModel>()
                .ForMember(c => c.Movies, opt => opt.MapFrom(src => GetMoviesForCast(src.MovieCasts)));

            CreateMap<Movie, MovieDetailsResponseModel>()
                .ForMember(md => md.Casts, opt => opt.MapFrom(src => GetCasts(src.MovieCasts)));

            CreateMap<User, UserRegisterResponseModel>();

            CreateMap<IEnumerable<Purchase>, PurchaseResponseModel>()
                .ForMember(p => p.PurchasedMovies, opt => opt.MapFrom(src => GetPurchasedMovies(src)))
                .ForMember(p => p.UserId, opt => opt.MapFrom(src => src.FirstOrDefault().UserId));

            CreateMap<IEnumerable<Favorite>, FavoriteResponseModel>()
                .ForMember(p => p.FavoriteMovies, opt => opt.MapFrom(src => GetFavoriteMovies(src)))
                .ForMember(p => p.UserId, opt => opt.MapFrom(src => src.FirstOrDefault().UserId));

            // Request Models to Db Entities Mappings
            CreateMap<PurchaseRequestModel, Purchase>();
            CreateMap<FavoriteRequestModel, Favorite>();
        }

        private List<FavoriteResponseModel.FavoriteMovieResponseModel> GetFavoriteMovies(
            IEnumerable<Favorite> favorites)
        {
            var favoriteResponse = new FavoriteResponseModel
                                   {
                                       FavoriteMovies = new List<FavoriteResponseModel.FavoriteMovieResponseModel>()
                                   };
            foreach (var favorite in favorites)
                favoriteResponse.FavoriteMovies.Add(new FavoriteResponseModel.FavoriteMovieResponseModel
                {
                                                         PosterUrl = favorite.Movie.PosterUrl,
                                                         Id = favorite.MovieId,
                                                         Title = favorite.Movie.Title
                                                     });

            return favoriteResponse.FavoriteMovies;
        }

        private List<PurchaseResponseModel.PurchasedMovieResponseModel> GetPurchasedMovies(
            IEnumerable<Purchase> purchases)
        {
            var purchaseResponse = new PurchaseResponseModel
                                   {
                                       PurchasedMovies =
                                           new List<PurchaseResponseModel.PurchasedMovieResponseModel>()
                                   };
            foreach (var purchase in purchases)
                purchaseResponse.PurchasedMovies.Add(new PurchaseResponseModel.PurchasedMovieResponseModel
                                                     {
                                                         PosterUrl = purchase.Movie.PosterUrl,
                                                         PurchaseDateTime = purchase.PurchaseDateTime,
                                                         Id = purchase.MovieId,
                                                         Title = purchase.Movie.Title
                                                     });

            return purchaseResponse.PurchasedMovies;
        }

        private List<MovieResponseModel> GetMoviesForCast(IEnumerable<MovieCast> srcMovieCasts)
        {
            var castMovies = new List<MovieResponseModel>();
            foreach (var movie in srcMovieCasts)
                castMovies.Add(new MovieResponseModel
                               {
                                   Id = movie.MovieId, PosterUrl = movie.Movie.PosterUrl, Title = movie.Movie.Title
                               });

            return castMovies;
        }


        private static List<MovieDetailsResponseModel.CastResponseModel> GetCasts(IEnumerable<MovieCast> srcMovieCasts)
        {
            var movieCast = new List<MovieDetailsResponseModel.CastResponseModel>();
            foreach (var cast in srcMovieCasts)
                movieCast.Add(new MovieDetailsResponseModel.CastResponseModel
                              {
                                  Id = cast.CastId,
                                  Gender = cast.Cast.Gender,
                                  Name = cast.Cast.Name,
                                  ProfilePath = cast.Cast.ProfilePath,
                                  TmdbUrl = cast.Cast.TmdbUrl
                              });

            return movieCast;
        }
    }
}