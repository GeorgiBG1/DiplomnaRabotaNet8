using Contracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class NotificationService : INotificationService
    {
        private readonly SkillBoxDbContext dbContext;

        public NotificationService(SkillBoxDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Notification[]> GetNotificationsAsync(string username)
        {
            var notifications = await dbContext.Notifications
                .Where(n => !n.IsDeleted)
                .Where(n => n.Owner.UserName == username)
                .OrderByDescending(n => n.CreatedOn)
                .Take(4)
                .ToArrayAsync();
            return notifications;
        }

        public void CreateNotification(string type, SkillBoxUser owner)
        {
            var notification = new Notification
            {
                Owner = owner
            };
            notification.SetImageURLAndSetContent(type);
            dbContext.Notifications.Add(notification);
            dbContext.SaveChanges();
        }
    }
}
