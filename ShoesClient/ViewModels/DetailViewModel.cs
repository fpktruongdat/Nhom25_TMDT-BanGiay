using ShoesClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesClient.ViewModels
{
    public class DetailViewModel
    {
        public Shoes shoes { get; set; }
        public int shoesId { get; set; }
        public float size { get; set; }
        public int quantity { get; set; }
    }
}
