using ShoesClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesClient.ViewModels
{
    public class CartLineViewModel
    {
        public int ID { get; set; }
        public int ShoesId { get; set; }
        public Shoes Shoes { get; set; }
        public int Quantity { get; set; }
        public float Size { get; set; }
        public int OrderId { get; set; }
    }
}
