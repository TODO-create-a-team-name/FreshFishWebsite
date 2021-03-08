
using Microsoft.AspNetCore.Mvc;

namespace FreshFishWebsite.ViewModels
{
    public class CreateStorageViewModel
    {
        public int StorageNumber { get; set; }
        public string Address { get; set; }
        [Remote(action: "CheckEmail", controller: "Storage", ErrorMessage = "уже існує адмін/водій з таким e-mail")]
        public string StorageAdminEmail { get; set; }
    }
}
