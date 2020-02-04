using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MovieShop.Core.ApiModels.Response;
using MovieShop.Core.Entities;

namespace MovieShop.Core.MappingProfiles
{
   public class MoviesMappingProfile: Profile
    {
        public MoviesMappingProfile()
        {
            CreateMap<Movie, MovieCardResponseModel>();
        }
    }
}
