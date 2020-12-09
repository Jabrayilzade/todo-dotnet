using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TodoWish.Dto;
using TodoWish.Models;

namespace TodoWish.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateTaskDto, Todo>();
            CreateMap<CreateProjectDto, Project>();
            CreateMap<User, LoginResultDto>();
        }
    }
}