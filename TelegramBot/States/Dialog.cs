using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.States
{
    public class Dialog : IUpdateState
    {
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, State state)
        {
	        if ((msg.Text.Equals("Плохо😒", StringComparison.OrdinalIgnoreCase)))
	        {
		        state.StateChat = StateChat.Location;
				await bot.SendTextMessageAsync(chatId, "Держи пончик🍩 \n грр");
			}
			else if ((msg.Text.Equals("Хорошо😃", StringComparison.OrdinalIgnoreCase)))
            {
	            state.StateChat = StateChat.Location;
				await bot.SendTextMessageAsync(chatId, "Рад за тебя!!!");
			}
			else if ((msg.Text.Equals("Отлично😁", StringComparison.OrdinalIgnoreCase)))
			{
				state.StateChat = StateChat.Location;
				await bot.SendTextMessageAsync(chatId, "Ну и отлично раз отлично)");
			}
            else if ((msg.Text.Equals("Домой🏚", StringComparison.OrdinalIgnoreCase)))
            {
                state.StateChat = StateChat.Main;
                await bot.SendTextMessageAsync(chatId, "Ты отправлен домой! Теперь тебе снова нужно написать привет");
            }

			else
            {
                await bot.SendTextMessageAsync(chatId, " Не понимаю тебя, если хочешь на главную просто нажми кнопку \"Домой🏚\"");
            }
        }

   

	}
}