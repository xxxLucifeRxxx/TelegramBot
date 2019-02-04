using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

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

		        await bot.SendTextMessageAsync(message.Chat.Id, "Ну привет, удиви МЕНЯ!!!");

            }

        }



	}
}
