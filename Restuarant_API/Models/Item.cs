using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restuarant_API.Models
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        [Column(TypeName="nvarchar(150)")]
        [Required]
        public string Name { get; set; }
        [Required]
        [Column(TypeName="decimal(18,2)")]
        public decimal Price { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
