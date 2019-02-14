using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.States
{
    public class Dialog : IUpdateState
    {
        public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, State state)
        {
            if (msg.Text == "У меня все хорошо!!!")
            {
                await bot.SendTextMessageAsync(chatId, "Рад за тебя!!!");
            }
            else if (msg.Text == "Домой")
            {
                state.StateChat = StateChat.Main;
                await bot.SendTextMessageAsync(chatId, "Ты отправолен домоЙ!!!");
            }
            else
            {
                await bot.SendTextMessageAsync(chatId, " Не понимсаю тебя, если хочешь на главную прсото скажи \"Домой\"");
            }
        }


    }
}