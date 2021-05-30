using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADF_TE_POC.DTOs
{
    public class ProductRequest
    {
        [Required]
        public string Name { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public decimal UnitPrice { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
