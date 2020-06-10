using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesClient.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Shoes shoes, int quantity,float size)
        {
            CartLine line = lineCollection.Where(s => s.Shoes.ShoesId == shoes.ShoesId && s.Size==size).FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine { CartLineID=shoes.ShoesId + size ,Shoes = shoes, Quantity = quantity, Size=size });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(float CartLineID) => lineCollection.RemoveAll(s => s.CartLineID == CartLineID);
        public virtual decimal ComputeTotalValue() => lineCollection.Sum(e => e.Shoes.Price * e.Quantity);

        public virtual void Clear() => lineCollection.Clear();
        public virtual int Count()
        {
            return lineCollection.Count;
        }

        public virtual IEnumerable<CartLine> Lines => lineCollection;



    }
    public class CartLine 
    {
        public float CartLineID { get; set; } 
        public Shoes Shoes { get; set; } 
        public int Quantity { get; set; } 
        public float Size { get; set; } 
    }

}
