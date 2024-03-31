using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotificationAPI.Domain;

namespace NotificationService.Data
{
    public class NotificationServiceContext : DbContext
    {
        public NotificationServiceContext (DbContextOptions<NotificationServiceContext> options)
            : base(options)
        {
        }

        public DbSet<NotificationAPI.Domain.Notification> Notification { get; set; } = default!;
    }
}
