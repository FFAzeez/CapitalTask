using AutoMapper;
using CapitalPlacementTaskAPI.Business.Commands;
using CapitalPlacementTaskAPI.Domain.BindingModels;
using CapitalPlacementTaskAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Business.Mapping
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProgramDetail, CreateProgramDetailCommand>().ReverseMap();
            CreateMap<ProgramDetail, ProgramDetailDTO>().ReverseMap();
            CreateMap<AdditionalProgramInformation, AdditionalProgramInformationCommand>().ReverseMap();
            CreateMap<AdditionalProgramInformation, AdditionalProgramInformationDTO>().ReverseMap();
        }
    }
}
