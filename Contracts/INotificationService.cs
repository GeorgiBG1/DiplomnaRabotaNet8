using Data.Models;

namespace Contracts
{
    public interface INotificationService
    {
        public Task<Notification[]> GetNotificationsAsync(string username);
        public void CreateNotification(string type, SkillBoxUser owner);
    }
}
