using AutoMapper;
using System;
using HotelManagement.Application;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.Domains;

namespace HotelManagement.Application.AtoMapper
{
    
    
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<UpdateUse, User>();
            }
        }
   
}
