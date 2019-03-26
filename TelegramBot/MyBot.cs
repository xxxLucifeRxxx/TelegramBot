using System;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBot.Model;

namespace TelegramBot
{
	internal static class Globals
	{
		public const string CallbackCancel = "callback";
		public const string CallbackCashPayment = "callback2";
		public const string CallbackMobileBank = "callback3";
		public const string CallbackCarSearch = "callback4";

		public static bool IsValidTime(string time)
		{
			string[] formats = { "HH:mm" };
			return DateTime.TryParseExact(time, formats, new CultureInfo("ru-RU"),
				DateTimeStyles.None, out _);
		}
	}

	internal class MyBot : IDisposable
	{
		private readonly Context _db = new Context();

		//private readonly List<State> _states;
		private static TelegramBotClient _bot;

		public MyBot(string token)
		{
			_bot = new TelegramBotClient(token);
			_bot.OnMessage += Bot_OnMessageReceived;
			var me = _bot.GetMeAsync().Result;
			Console.WriteLine("I'm alive " + me.FirstName);
		}

		public void Start()
		{
			Console.WriteLine("Try start Receiving ...");
			_bot.StartReceiving();
			Console.WriteLine("Start Receiving");
		}

		public void Stop()
		{
			_bot.StopReceiving();
		}

		private async void Bot_OnMessageReceived(object sender, MessageEventArgs e)
		{
			await Task.Factory.StartNew(() => new Fsm(e.Message.Chat.Id, e.Message, _bot, _db));
		}

		public void Dispose()
		{
			_db.Dispose();
		}
	}
}