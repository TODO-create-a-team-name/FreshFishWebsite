﻿
using Microsoft.AspNetCore.Mvc;

namespace FreshFishWebsite.ViewModels
{
    public class StorageViewModel
    {
        public int Id { get; set; }
        public int StorageNumber { get; set; }
        public string Address { get; set; }
        [Remote(action: "CheckEmail", controller: "Storage", ErrorMessage = "уже існує адмін/водій з таким e-mail")]
        public string StorageAdminEmail { get; set; }
    }
}
