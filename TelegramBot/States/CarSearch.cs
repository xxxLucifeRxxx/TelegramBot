﻿using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.States
{
    class CarSearch
    {
        public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, State state)
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