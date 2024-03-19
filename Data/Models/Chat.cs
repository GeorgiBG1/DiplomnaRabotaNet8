﻿using Data.Models;

namespace Data.Models
{
    public class Chat : BaseEntity<string>
    {
        public Chat()
        {
            Id = Guid.NewGuid().ToString();
            ChatUsers = new HashSet<ChatUser>();
            Messages = new HashSet<UserMessage>();
        }
        public string Name { get; set; }
        public int ServiceId { get; set; }
        public virtual SkillBoxService Service { get; set; }
        public virtual ICollection<ChatUser> ChatUsers { get; set; }
        public virtual ICollection<UserMessage> Messages { get; set; }
    }
}
