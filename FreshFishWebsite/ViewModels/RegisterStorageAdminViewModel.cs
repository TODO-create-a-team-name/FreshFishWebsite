using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.ViewModels
{
    public class RegisterStorageAdminViewModel : RegisterViewModel
    {
        public int StorageId { get; set; }

        public string StorageAddress { get; set; }

        public string StorageAdminEmail { get; set; }
    }
}
