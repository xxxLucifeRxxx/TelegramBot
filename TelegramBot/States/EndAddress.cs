using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Enumerations;
using TelegramBot.Model;

namespace TelegramBot.States
{
	public class EndAddress : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, Context db)
		{

            if (msg.Type != MessageType.Venue)
			{
			    await bot.SendTextMessageAsync(
			        chatId: msg.Chat.Id,
			        text: "Отправленное вами сообщение не является адресом");
            }
			else if (msg.Text.Equals("Отмена", StringComparison.OrdinalIgnoreCase))
			{
				var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
				if (user != null)
					user.State = StateChatEnum.Cancel;
			}
			else
			{
			    await bot.SendTextMessageAsync(chatId,
			        text: "Теперь назначьте пожалуйста время на которое желаете заказать такси. \n " +
			              "Формат для отправки времени 'hh:mm \n" +
			              "Где hh-часы, mm-минуты'",
					replyMarkup: new ReplyKeyboardMarkup(new[]
			        {
				        new KeyboardButton("Заказать на сейчас"),
				        new KeyboardButton("Отмена"),
					}, true, true));

				var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
				if (user != null)
					user.State = StateChatEnum.SendingTime;
			}
		}
	}
}

/*(msg.Text.Equals("Плохо😒", StringComparison.OrdinalIgnoreCase))*/