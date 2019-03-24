using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Enumerations;
using TelegramBot.Model;
using System.Linq;

namespace TelegramBot.States
{
	public class SendingTime : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, Context db)
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
			else if (msg.Text.Equals("Отмена", StringComparison.OrdinalIgnoreCase))
            {
	            var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
	            if (user != null)
		            user.State = StateChatEnum.Cancel;
			}
			else if (msg.Text.Equals("Заказать на сейчас", StringComparison.OrdinalIgnoreCase))
            {
	            var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
	            if (user != null)
		            user.State = StateChatEnum.Time;
			}
			else
			{
			    await bot.SendTextMessageAsync(chatId,
			        "Теперь укажите пожалуйста ваш номер телефона",
			        replyMarkup:requestReplyKeyboard);

				var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
				if (user != null)
					user.State = StateChatEnum.PaymentMethod;
			}
		}
	}
}
