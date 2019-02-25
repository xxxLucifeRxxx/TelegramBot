using System;
using Telegram.Bot;

namespace TelegramBot
{
    public class Program
    {
        private static ITelegramBotClient bot;

        private static void Main(string[] args)
        {
            const string Token = "769411125:AAGApaP9Rh1ohzIytQBajfA74OXwBvE70h0";
            var bot = new MyBot(Token);
          
            bot.Start();
            Console.ReadLine();
        }
    }
}