using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesAPI.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

       

        public virtual void Clear() => lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }
    public class CartLine
    {
        public int ID { get; set; }
        public int ShoesId { get; set; }
        public Shoes Shoes { get; set; }
        public int Quantity { get; set; }
        public float Size { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
