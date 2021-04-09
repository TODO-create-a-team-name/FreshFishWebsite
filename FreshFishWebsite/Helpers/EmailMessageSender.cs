using FreshFishWebsite.Services;
using MimeKit.Cryptography;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FreshFishWebsite.Helpers
{
    public static class EmailMessageSender
    {
        private readonly static EmailService _emailService;

        static EmailMessageSender()
        {
            _emailService = new();
        }

        public static async Task SendNewOrderMessageAsync(string emailTo)
        {
            await _emailService.SendEmailAsync("freshfishofficial@gmail.com", "Нове замовлення",
                       $"Нове замовлення від {emailTo}");
        }

        public static async Task SendEmailConfirmationMessageAsync(string callbackUrl, string email)
        {
            await _emailService.SendEmailAsync(email, "Підтвердіть свій акаунт",
                        $"Підтвердіть свій акаунт за наступним посиланням: <a href='{callbackUrl}'>Підтвердити акаунт</a>");
        }

        public static async Task SendChangePasswordMessageAsync(string email, string callbackUrl)
        {
            await _emailService.SendEmailAsync(email,
                    "Зміна паролю",
                    $"Перейдіть за наступним посиланням, щоб змінити ваш пароль:" +
                    $" <a href='{callbackUrl}'>Змінити пароль</a>");
        }

        public static async Task SendThatUserBecameStorageAdminMessageAsync(string email, int storageNumber)
        {
            await _emailService.SendEmailAsync(email, "Адміністратор складу FreshFish",
                       $"Ви тепер адміністратор складу №{storageNumber}");
        }

        public static async Task SendStorageAdminStatusConfirmationMessage(string email, int storageNumber, string callbackUrl)
        {
            await _emailService.SendEmailAsync(email, "Адміністратор складу FreshFish",
                       $"Тепер ви адміністратор складу №{storageNumber}: <a href='{callbackUrl}'>Підтвердити</a>");
        }
        public static async Task SendThatUserBecameDriverMessageAsync(string email, int storageNumber)
        {
            await _emailService.SendEmailAsync(email, "Водій складу FreshFish",
                       $"Ви тепер водій складу №{storageNumber}");
        }

        public static async Task SendDriverStatusConfirmationMessage(string email, int storageNumber, string callbackUrl)
        {
            await _emailService.SendEmailAsync(email, "Водій складу FreshFish",
                       $"Тепер ви водій складу №{storageNumber}: <a href='{callbackUrl}'>Підтвердити</a>");
        }
    }
}
