using System;
using System.Linq;
using TelegramBot.Model;

namespace TelegramBot
{
	public class Program
	{
		private static void Main()
		{
			//const string token = "769411125:AAGApaP9Rh1ohzIytQBajfA74OXwBvE70h0";
			//var bot = new MyBot(token);

			//bot.Start();
			var db = new Context();
			var users = db.Users.ToList();
			Console.ReadLine();
		}
	}
}