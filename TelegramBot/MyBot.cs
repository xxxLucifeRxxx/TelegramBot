using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
	internal static class Helper
	{
		public static string data;
	}

	internal class MyBot
	{
		public const string CallbackCancel = "callback";
		public const string CallbackLocation = "callback1";
		private readonly List<State> states = new List<State>();
		private static TelegramBotClient bot;

		public MyBot(string token)
		{
			bot = new TelegramBotClient(token);
			bot.OnMessage += Bot_OnMessageReceivede;
			bot.OnCallbackQuery += async (object sender, CallbackQueryEventArgs e) =>
			{
				if (e.CallbackQuery.Data == CallbackCancel)
				{
					Helper.data = e.CallbackQuery.Id;
					var state = states.FirstOrDefault(x => x.ChatId == e.CallbackQuery.Message.Chat.Id);
					state.StateChat = StateChat.Cancel;
					var fsm = new Fsm(e.CallbackQuery.Message.Chat.Id, states, e.CallbackQuery.Message, bot);
				}
			};
			var me = bot.GetMeAsync().Result;
			Console.WriteLine("I'm alive " + me.FirstName);
		}

		//public void UpdateAsync(Message msg, long chatId, State state)
		//{
		//	bot.OnCallbackQuery += async (object sender, CallbackQueryEventArgs e) =>
		//	{
		//		if (e.CallbackQuery.Data == CallbackCancel)
		//		{
		//			await bot.AnswerCallbackQueryAsync(
		//				callbackQueryId: CallbackCancel,
		//				"Заказ отменен",
		//				true, null, 30);

		//			await bot.SendTextMessageAsync(
		//				chatId: msg.Chat.Id,
		//				text: "Заказ отменен");
		//			state.StateChat = StateChat.StartMain;
		//		}
		//	};
		//}

		public void Start()
		{
			Console.WriteLine("Try start Reciveing ...");
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