﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfShoppingCart
{
    public class OrderDetails
    {
        public int orderId { get; set; }
        public int productId { get; set; }
        public int Quantity { get; set; }
        public int TotalCost { get; set; }
    }
}
