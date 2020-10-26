using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace web.Classes
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Data Source=blogging.db");
    }

    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
	public string Password { get; set; }

        public List<Message> Messages { get; } = new List<Message>();
    }

    public class Message
    {
        public int MessageID { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
    }
}
