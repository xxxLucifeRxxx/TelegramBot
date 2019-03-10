using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramBot
{
	internal static class Globals
	{
		public static string Data;
		public const string CallbackCancel = "callback";
	}

	internal class MyBot
	{
		private readonly List<State> _states = new List<State>();
		private static TelegramBotClient _bot;

		public MyBot(string token)
		{
			_bot = new TelegramBotClient(token);
			_bot.OnMessage += Bot_OnMessageReceived;
			_bot.OnCallbackQuery += ( sender, e) =>
			{
				if (e.CallbackQuery.Data == Globals.CallbackCancel)
				{
					Globals.Data = e.CallbackQuery.Id;
					var state = _states.FirstOrDefault(x => x.ChatId == e.CallbackQuery.Message.Chat.Id);
					if (state != null)
						state.StateChat = StateChat.Cancel;
					var fsm = new Fsm(e.CallbackQuery.Message.Chat.Id, _states, e.CallbackQuery.Message, _bot);
				}
			};
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
			await Task.Factory.StartNew(() => new Fsm(e.Message.Chat.Id, _states, e.Message, _bot));
		}
	}
}