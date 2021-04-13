using FreshFishWebsite.Models;
using System.ComponentModel.DataAnnotations;

namespace FreshFishWebsite.ViewModels
{
    public class DetailedPoolViewModel
    {
        public Pool Pool { get; set; }
        public int ProductsKgSum { get; set; }
        public int FishQuantly { get; set; }
    }
}
