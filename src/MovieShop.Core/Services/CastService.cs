﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MovieShop.Core.ApiModels.Response;
using MovieShop.Core.Entities;
using MovieShop.Core.Exceptions;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Core.Services
{
   public class CastService: ICastService
    {
        private readonly ICastRepository _castRepository;
        private readonly IMapper _mapper;

        public CastService(ICastRepository castRepository, IMapper mapper)
        {
            _castRepository = castRepository;
            _mapper = mapper;
        }
        public async Task<CastDetailsResponseModel> GetCastDetailsWithMovies(int id)
        {
           var cast =  await _castRepository.GetByIdAsync(id);
           if (cast ==null)
           {
               throw new NotFoundException("Cast", id);
           }

           var response = _mapper.Map<CastDetailsResponseModel>(cast);
           return response;
        }
    }
}
