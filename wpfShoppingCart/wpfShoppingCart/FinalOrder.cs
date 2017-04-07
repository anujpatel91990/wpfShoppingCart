using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfShoppingCart
{
    public class FinalOrder
    {
        public string imageUrl { get; set; }
        public string productName { get; set; }
        public int productId { get; set; }
        public int Quantity { get; set; }
        public int TotalCost { get; set; }
    }
}
