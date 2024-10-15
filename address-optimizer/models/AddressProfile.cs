using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace address_optimizer.models
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Dadata.Model.Address, AddressDto>();
        }
    }
}