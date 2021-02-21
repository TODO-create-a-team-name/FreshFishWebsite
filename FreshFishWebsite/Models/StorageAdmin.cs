

using System.ComponentModel.DataAnnotations.Schema;

namespace FreshFishWebsite.Models
{
    [Table("StorageAdmins")]
    public class StorageAdmin : User
    {
        public Storage Storage { get; set; }
    }
}
