using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
	internal class MyBot
	{
		public const string CallbackLocation = "callback1";
		private readonly List<State> states = new List<State>();
		private static TelegramBotClient bot;

		public MyBot(string Token)
		{
			bot = new TelegramBotClient(Token);
			bot.OnMessage += Bot_OnMessageReceivede;
			bot.OnCallbackQuery += async (object sender, CallbackQueryEventArgs e) =>
			{
				var message = e.CallbackQuery.Message;
				if (e.CallbackQuery.Data == CallbackLocation)
				{
					await Task.Factory.StartNew(() =>
					{
						var RequestReplyKeyboard = new ReplyKeyboardMarkup(new[]
						{
							KeyboardButton.WithRequestLocation(CallbackLocation),
						});
					});
				}
				else if (e.CallbackQuery.Data == "callback2")
				{
				}
			};
			var me = bot.GetMeAsync().Result;
			Console.WriteLine("I'm alive " + me.FirstName);
		}

		public void Start()
		{
			Console.WriteLine("Try start Recieveing ...");
			bot.StartReceiving();
			Console.WriteLine("Start Recieveing");
		}

		public void Stop()
		{
			bot.StopReceiving();
		}

		private async void Bot_OnMessageReceivede(object sender, MessageEventArgs e)
		{
			await Task.Factory.StartNew(() =>
			{
				var fsm = new Fsm(e.Message.Chat.Id, states, e.Message, bot);
			});
		}
	}
}