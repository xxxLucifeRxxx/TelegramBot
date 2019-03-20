using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBot.Enumerations;

namespace TelegramBot
{
	internal static class Globals
	{
		public static string Data;//Переменная для передачи Id запроса callback, используется пока нету БД
		public const string CallbackCancel = "callback";
		public const string CallbackTime = "callback1";
	    public const string CallbackCashPayment = "callback2";
	    public const string CallbackMobileBank = "callback3";
	    public const string CallbackCarSearch = "callback4";

        public static int MessageId; //Для удаления Inline кнопок в дальнейшем
	    public static string ArrivalTime;  

	    
        public static bool IsValidTime(string time)
	    {
	        string[] formats = { "HH:mm" };
	        return DateTime.TryParseExact(time, formats, new CultureInfo("ru-RU"),
	            DateTimeStyles.None, out _);
	    }
    }

	internal class MyBot
	{
		private readonly List<State> _states = new List<State>();
		private static TelegramBotClient _bot;

		public MyBot(string token)
		{
            _bot = new TelegramBotClient(token);
			_bot.OnMessage += Bot_OnMessageReceived;
			_bot.OnCallbackQuery += (sender, e) =>
			{
			    switch (e.CallbackQuery.Data)
			    {
			        case Globals.CallbackCancel:
			        {
			            Globals.Data = e.CallbackQuery.Id;
			            var state = _states.FirstOrDefault(x => x.ChatId == e.CallbackQuery.Message.Chat.Id);
			            if (state != null)
			                state.StateChatEnum = StateChatEnum.Cancel;
			            // ReSharper disable once ObjectCreationAsStatement
			            new Fsm(e.CallbackQuery.Message.Chat.Id, _states, e.CallbackQuery.Message, _bot);
			            break;
			        }
			        case Globals.CallbackTime:
			        {
			            Globals.ArrivalTime = "Ближайшее время";
			            Globals.Data = e.CallbackQuery.Id;
			            var state = _states.FirstOrDefault(x => x.ChatId == e.CallbackQuery.Message.Chat.Id);
			            if (state != null)
			                state.StateChatEnum = StateChatEnum.Time;
			            // ReSharper disable once ObjectCreationAsStatement
			            new Fsm(e.CallbackQuery.Message.Chat.Id, _states, e.CallbackQuery.Message, _bot);
			            break;
			        }
			        case Globals.CallbackCashPayment:
			        {
			            Globals.Data = e.CallbackQuery.Id;
			            var state = _states.FirstOrDefault(x => x.ChatId == e.CallbackQuery.Message.Chat.Id);
			            if (state != null)
			                state.StateChatEnum = StateChatEnum.CarSearch;
			            // ReSharper disable once ObjectCreationAsStatement
			            new Fsm(e.CallbackQuery.Message.Chat.Id, _states, e.CallbackQuery.Message, _bot);
			            break;
			        }
			        case Globals.CallbackMobileBank:
			        {
			            Globals.Data = e.CallbackQuery.Id;
			            var state = _states.FirstOrDefault(x => x.ChatId == e.CallbackQuery.Message.Chat.Id);
			            if (state != null)
			                state.StateChatEnum = StateChatEnum.CarSearch;
			            // ReSharper disable once ObjectCreationAsStatement
			            new Fsm(e.CallbackQuery.Message.Chat.Id, _states, e.CallbackQuery.Message, _bot);
			            break;
			        }
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