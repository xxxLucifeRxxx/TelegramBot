using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramBot
{
	class Program
	{
		static TelegramBotClient bot;

		static void Main(string[] args)
		{
			
			bot = new TelegramBotClient("769411125:AAGASD2jpK-_vnG_VSu6RJ8yFMWofdBcCS0");

			bot.OnMessage += Bot_OnMessageReceived;

			var me = bot.GetMeAsync().Result;

			Console.WriteLine(me.FirstName);
			bot.StartReceiving();
			Console.ReadLine();
			bot.StopReceiving();
		}

		private static void Bot_OnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
		{
			var message = e.Message;

			Console.WriteLine(message.Text);
		}
	}
}
