using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesAPI.Models
{
    public class ShoesModel
    {
        public int ShoesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile ImageUrl { get; set; }
        public IFormFile ImageThumbnailUrl { get; set; }
        public bool IsOnSale { get; set; }
        public bool IsOnNew { get; set; }
        public bool IsOnStock { get; set; }
        public int CategoryId { get; set; }

    }
}
