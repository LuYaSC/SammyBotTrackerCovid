using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data.Migration
{
    public class SendNotificationMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SendNotification>().Property(prop => prop.SendPhone).HasColumnType("nvarchar").HasMaxLength(15).IsRequired();
            modelBuilder.Entity<SendNotification>().Property(prop => prop.UID).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<SendNotification>().Property(prop => prop.MessageStatus).HasColumnType("text");
        }
    }
}
