using System.Collections.Generic;
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
            CreateMap<Movie, MovieCardResponseModel>();
            CreateMap<Cast, CastDetailsResponseModel>()
                .ForMember(c => c.Movies, opt => opt.MapFrom(src => GetMoviesForCast(src.MovieCasts)));

            CreateMap<Movie, MovieDetailsResponseModel>()
                .ForMember(md => md.Casts, opt => opt.MapFrom(src => GetCasts(src.MovieCasts)));

            CreateMap<User, UserRegisterResponseModel>();


            // Request Models to Db Entities Mappings
            CreateMap<PurchaseRequestModel, Purchase>();
            CreateMap<FavoriteRequestModel, Favorite>();
        }

        private List<MovieCardResponseModel> GetMoviesForCast(IEnumerable<MovieCast> srcMovieCasts)
        {
            var castMovies = new List<MovieCardResponseModel>();
            foreach (var movie in srcMovieCasts)
                castMovies.Add(new MovieCardResponseModel
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