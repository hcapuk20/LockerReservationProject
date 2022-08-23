using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LRProject.Models;

namespace LRProject.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, ReturnEmployeeDTO>();
            //CreateMap<List<Employee>, List<ReturnEmployeeDTO>>();

            CreateMap<Source, ReturnSourceDTO>();

            CreateMap<SourceGroup, ReturnSourceGroupDTO>();

            CreateMap<Employee, EmpDTOIdName>();

            CreateMap<Source, GetSourcesByGroupDTO>();


        }
    }
}