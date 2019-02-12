using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
	class Program
	{
		static TelegramBotClient bot;
	    private string _key;

     //   public TestBot(string key)
	    //{
	    //    _key = key;

	    //}

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

		private static async void Bot_OnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
		{
		    StringBuilder msg = new StringBuilder();
            var message = e.Message;

			if (message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
			{
				if (message.Text.ToLower() != "привет")
				{
					Console.WriteLine(message.Text);

					msg.AppendLine(
						"Я не понимаю того что вы мне написали, мой создатель не наделил меня сверхразумом)");
					msg.AppendLine(
						"Введите слово привет для начала работы со мной.");

					await bot.SendTextMessageAsync(message.Chat.Id, msg.ToString());
				}
				else
				{

					await bot.SendTextMessageAsync(message.Chat.Id, "Ну привет, удиви МЕНЯ!!!(/help)");

					if(message.Text == "/help")
					{
						var keyboard = new InlineKeyboardMarkup(new Telegram.Bot.Types.InlineKeyboardButton[][]
										{new [] {
												 new Telegram.Bot.Types.InlineKeyboardButton("Текст для первой кнопки","callback1"),
												 new Telegram.Bot.Types.InlineKeyboardButton("Текст второй кнопки","callback2"),
												 },
									   });

						bot.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>
						{
							var messages = ev.CallbackQuery.Message;
							if (ev.CallbackQuery.Data == "callback1")
							{
								// сюда то что нужно сделать при нажатии на первую кнопку 
							}
							else
							if (ev.CallbackQuery.Data == "callback2")
							{
								// сюда то что сделать при нажатии на вторую кнопку
							}
						};
					}




				}
			}

        }



	}
}
