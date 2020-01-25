using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restuarant_API.Models
{
    public class Order
    {
        [Key]
        public long OrderID { get; set; }

        [Required]
        public string OrderNo { get; set; }
        [Required]

        public string PMethod { get; set; }


        public decimal GTotal { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public bool IsCompleted { get; set; }

        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
