using System;
using System.ComponentModel.DataAnnotations;

namespace FreshFishWebsite.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
