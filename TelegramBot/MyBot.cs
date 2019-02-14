using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramBot
{
    internal class MyBot
    {
        private readonly List<State> states = new List<State>();
        private TelegramBotClient bot;

        public MyBot(string Token)
        {
            bot = new TelegramBotClient(Token);
            bot.OnMessage += Bot_OnMessageReceivede;
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

        private async void Bot_OnMessageReceivede(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            await Task.Factory.StartNew(() =>
            {
                var fsm = new Fsm(e.Message.Chat.Id, states, e.Message, bot);
            });
        }
    }
}