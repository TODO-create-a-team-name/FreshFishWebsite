namespace FreshFishWebsite.ViewModels
{
    public class RegisterStorageWorker : RegisterViewModel
    {
        public int StorageId { get; set; }

        public int StorageNumber { get; set; }

        public string StorageAddress { get; set; }

        public string WorkerEmail { get; set; }
    }
}
