using System.Collections.Generic;
using AutoMapper;
using MovieShop.Core.ApiModels.Response;
using MovieShop.Core.Entities;

namespace MovieShop.Core.MappingProfiles
{
    public class MoviesMappingProfile : Profile
    {
        public MoviesMappingProfile()
        {
            CreateMap<Movie, MovieCardResponseModel>();

            CreateMap<Movie, MovieDetailsResponseModel>()
                .ForMember(md => md.Casts, opt => opt.MapFrom(src => GetCasts(src.MovieCasts)));
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