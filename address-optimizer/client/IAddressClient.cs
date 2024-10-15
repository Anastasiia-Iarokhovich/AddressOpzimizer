using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using address_optimizer.models;

namespace address_optimizer.client
{
    public interface IAddressClient
    {
        public Task<List<AddressDto>> GetAll();
        public Task<AddressDto> GetById(int id);
        public Task<AddressDto> Post();
    }
}