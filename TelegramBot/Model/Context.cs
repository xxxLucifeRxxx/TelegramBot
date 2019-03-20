using System.Data.Entity;

namespace TelegramBot.Model
{
	public class Context : DbContext
	{
		public Context() : base("db_bot")
		{
			Database.SetInitializer(new Class1());
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Application> Requests { get; set; }
		public DbSet<Driver> Drivers { get; set; }
	}
}