using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.MainFunctionality;
using TelegramBot.Model;

namespace TelegramBot.States
{
    class CarSearch
    {
        public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, Context db)
        {
            var keyboardApplication = new InlineKeyboardMarkup(new[]
            {

                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Поиск водителя", Globals.CallbackCarSearch),

                }
            });

            await bot.SendTextMessageAsync(
                chatId: msg.Chat.Id,
                text: "Способ оплаты выбран",
                replyMarkup: keyboardApplication);
        }
    }
}