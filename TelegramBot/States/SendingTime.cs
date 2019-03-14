using System;
using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.States
{
	public class SendingTime : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, State state)
		{
		    var requestReplyKeyboard = new ReplyKeyboardMarkup(new[]
		    {
		        KeyboardButton.WithRequestContact("Отправить текущий номер"),
		    }, true, true);

            if (!Globals.IsValidTime(msg.Text))
			{
				await bot.SendTextMessageAsync(chatId,
					 "Вы не правильно указали время");
			}
			else
			{
			    await bot.SendTextMessageAsync(chatId,
			        "Теперь укажите пожалуйста ваш номер телефона",
			        replyMarkup:requestReplyKeyboard);
			    state.StateChat = StateChat.PaymentMethod;
            }
		}
	}
}
