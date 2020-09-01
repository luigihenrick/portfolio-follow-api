using AutoMapper;
using PortfolioFollow.Domain.Classes.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioFollow.Api.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<FixedIncomeRequest, FixedIncomeServiceRequest>().ReverseMap();
            CreateMap<TreasureDirectRequest, TreasureDirectServiceRequest>().ReverseMap();
            CreateMap<VariableIncomeRequest, VariableIncomeServiceRequest>().ReverseMap();
        }
    }
}
