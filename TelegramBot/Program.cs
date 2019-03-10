using System;

namespace TelegramBot
{
	public class Program
	{

		private static void Main()
		{
			const string token = "769411125:AAGApaP9Rh1ohzIytQBajfA74OXwBvE70h0";
			var bot = new MyBot(token);

			bot.Start();
			Console.ReadLine();
		}
	}
}