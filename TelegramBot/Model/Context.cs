using System.Data.Entity;

namespace TelegramBot.Model
{
	public class Context : DbContext
	{
        public Context() : base("db_bot")
        {
        }

        public DbSet<User> Users { get; set; }
		public DbSet<Application> Applications { get; set; }
		public DbSet<Driver> Drivers { get; set; }
	}
}