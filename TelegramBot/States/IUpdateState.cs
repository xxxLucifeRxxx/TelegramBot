using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.States
{
    internal interface IUpdateState
    {
        void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, State state);
    }
}