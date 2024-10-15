using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using address_optimizer.client;
using address_optimizer.models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace address_optimizer.controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private readonly IAddressClient _addressClient;

        public AddressController(ILogger<AddressController> logger, 
                                IAddressClient addressClient)
        {
            _logger = logger;
            _addressClient = addressClient;
        }

        // public IActionResult Index()
        // {
        //     return View();
        // }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAll()
        {
            try
            {
                var addresses = await _addressClient.GetAll();
                return Ok(addresses);
            }
            catch (HttpRequestException e)
            {
                //Console.WriteLine("Failed to retrieve addresses: {e}", e.Message);
                _logger.LogError("Failed to retrieve addresses: {e}", e.Message);
                return StatusCode(500, "Internal Server Error");
            } 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDto>> GetById(int id)
        {
            try
            {
                var address = await _addressClient.GetById(id);
                return Ok(address);
            }
            catch (HttpRequestException e)
            {
                //Console.WriteLine($"Error getting address with ID {id}: {e.Message}");
                _logger.LogError($"Error getting address with ID { id}: { e.Message}");
                return StatusCode(404, "Not found");
            }
        }
            
        [HttpPost]
        public async Task<ActionResult<AddressDto>> Post()
        {
            try
            {
                var address = await _addressClient.Post();
                return Ok(address);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError("Failed to retrieve addresses: {e}", e.Message);
                return StatusCode(404, "Not found");
            }
        } 
    }
}