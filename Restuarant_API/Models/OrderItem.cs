using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restuarant_API.Models
{
    public class OrderItem
    {   
        [Key]
        public long OrderItemID { get; set; }
    
        public int Quantity { get; set; }
        public long OrderID { get; set; }
        public Order Order { get; set; }

        public int ItemID { get; set; }
        public Item Item { get; set; }

    }
}
