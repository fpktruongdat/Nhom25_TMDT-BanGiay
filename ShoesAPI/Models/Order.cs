using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public List<CartLine> MyCart { get; set; }

        public string UserId { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

    }
}
