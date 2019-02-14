using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.States
{
    public class MainState : IUpdateState
    {
        public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, State state)
        {
            if (msg.Text.Equals("привет",StringComparison.OrdinalIgnoreCase))
            {
                await bot.SendTextMessageAsync(chatId, "Hi ,men, Как дела?");
                state.StateChat = StateChat.Dialog;
            }
            else
            {
                await bot.SendTextMessageAsync(chatId, "Ввод неверной информации, повторитие");
            }
        }


    }
}