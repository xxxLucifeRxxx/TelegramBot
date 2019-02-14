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
        private readonly List<State> states = new List<State>();
        private static TelegramBotClient bot;

        public MyBot(string Token)
        {
            bot = new TelegramBotClient(Token);
            bot.OnMessage += Bot_OnMessageReceivede;
            bot.OnInlineQuery += BotOnInlineQueryReceived;
			var me = bot.GetMeAsync().Result;
            Console.WriteLine("I'm alive " + me.FirstName);
        }

        private static async void BotOnInlineQueryReceived(object sender, InlineQueryEventArgs inlineQueryEventArgs)
		{

			Console.WriteLine($"Получен встроенный запрос от: {inlineQueryEventArgs.InlineQuery.From.Id}");

			InlineQueryResultBase[] results =
			{
				new InlineQueryResultLocation(
						id: "1",
						latitude: 40.7058316f,
						longitude: -74.2581888f,
						title: "New York") // отображаемый результат
					{
						InputMessageContent = new InputLocationMessageContent(
							latitude: 40.7058316f,
							longitude: -74.2581888f) // сообщение, если выбран результат
					},

				new InlineQueryResultLocation(
						id: "2",
						latitude: 13.1449577f,
						longitude: 52.507629f,
						title: "Berlin") // отображаемый результат
					{
						InputMessageContent = new InputLocationMessageContent(
							latitude: 13.1449577f,
							longitude: 52.507629f) // сообщение, если выбран результат
					}
			};

				await bot.AnswerInlineQueryAsync(
				inlineQueryEventArgs.InlineQuery.Id,
				results,
				isPersonal: true,
				cacheTime: 0);
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