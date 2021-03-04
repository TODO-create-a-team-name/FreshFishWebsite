
using Microsoft.AspNetCore.Mvc;

namespace FreshFishWebsite.ViewModels
{
    public class CreateStorageViewModel
    {
        public int StorageNumber { get; set; }
        public string Address { get; set; }
        [Remote(action: "CheckEmailAsync", controller: "Storage")]
        public string StorageAdminEmail { get; set; }
    }
}
