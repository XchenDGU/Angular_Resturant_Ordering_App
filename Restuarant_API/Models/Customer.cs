using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restuarant_API.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required]
        [Column(TypeName="nvarchar(150)")]
        public string Name { get; set; }

        
        public ICollection<Order> Orders { get; set; }
    }
}
