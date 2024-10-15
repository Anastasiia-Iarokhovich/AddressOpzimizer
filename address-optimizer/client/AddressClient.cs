using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using address_optimizer.models;
using AutoMapper;
using Dadata;
using Dadata.Model;
using Newtonsoft.Json;

namespace address_optimizer.client
{
    public class AddressClient : IAddressClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        public readonly IMapper _mapper;

        public AddressClient(IHttpClientFactory httpClientFactory,
                            IConfiguration config, 
                            IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
            _mapper = mapper;
        }

        public async Task<List<AddressDto>> GetAll()
        {
            var client = _httpClientFactory.CreateClient("HttpAddressClient");

            var addresses = await client.GetFromJsonAsync<List<AddressDto>>("addresses");

            return addresses;
        }

        public async Task<AddressDto> GetById(int id)
        {
            var client = _httpClientFactory.CreateClient("HttpAddressClient");

            var address = await client.GetFromJsonAsync<AddressDto>($"addresses/{id}");

            return address;   
        }

        public async Task<AddressDto> Post()
        {
            var address = await GetById(3);

            var input = address.ToString();

            var result = await TransformWithDadata(input);

            CreateJsonFile(result);

            AddressDto addressDto = _mapper.Map<AddressDto>(result);

            return addressDto;
        }

        private async Task<Address> TransformWithDadata(string input)
        {
            var api = new CleanClientAsync(_config["DadataApiKey"], _config["DadataSecretKey"]);

            var result = await api.Clean<Address>(input);

            return result;
        }

        private void CreateJsonFile(Address address)
        {
            var jsonFile = JsonConvert.SerializeObject(address);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "result.json");

            File.WriteAllText(path, jsonFile);
        }
    }
}