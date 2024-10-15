using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace address_optimizer.models
{
    public class AddressDto
    {
        public int id { get; set; }
        public string street { get; set; }
        public string house { get; set; }
        public string flat { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string postal_code { get; set; }

        public override string ToString()
        {
            return $"{city} {country} {flat} {house} {street} {postal_code}";
        }
    }
}