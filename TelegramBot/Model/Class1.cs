using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Model
{
	// Для тестов бд
	public class Class1 : DropCreateDatabaseIfModelChanges<Context>
	{
		protected override void Seed(Context context)
		{
			context.Drivers.Add(new Driver());
			context.SaveChanges();
			base.Seed(context);
		}
	}
}