using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Enumerations;
using TelegramBot.Model;
using System.Linq;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.MainFunctionality;

namespace TelegramBot.States
{
	public class SendingTime : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, Context db)
		{
			if (Globals.IsValidTime(msg.Text))
			{
				var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
				var application = db.Applications.FirstOrDefault(x => x.UserId == user.UserId);
				if (application != null && user != null)
				{
						application.Time = DateTime.Parse(msg.Text);
						user.State = StateChatEnum.SendingNumberPhone;
				}
				await bot.SendTextMessageAsync(chatId,
				$"Время {msg.Text} будет указано в заявке \n" +
				$"Теперь нажав на кнопку Далее разрешите отправку вашего номера," +
				$"он будет указан в заявке для связи водителя с вами",
				replyMarkup: new ReplyKeyboardMarkup(new[]
				{
					KeyboardButton.WithRequestContact("Далее"),
					new KeyboardButton("Отмена")
				}, true, true));
				db.SaveChanges();
			}
			else
			{
				if (msg.Text.Equals("Отмена", StringComparison.OrdinalIgnoreCase))
				{
					var send = new SendMessageRequest(msg.Chat.Id,
						"Вы отменили заказ")
					{
						ReplyMarkup = new ReplyKeyboardRemove(),
					};
					await bot.MakeRequestAsync(send);

					var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
					var application = db.Applications.FirstOrDefault(x => x.UserId == user.UserId);
					db.Applications.Remove(application ?? throw new InvalidOperationException());
					if (user != null)
						user.State = StateChatEnum.StartMain;
					db.SaveChanges();
				}
				else if (msg.Text.Equals("Далее", StringComparison.OrdinalIgnoreCase))
				{
					await bot.SendTextMessageAsync(
						msg.Chat.Id,
						text: "Текущее время отправлено. \n" +
							  "Теперь укажите пожалуйста ваш номер телефона. " +
							  "Для этого нажмите Далее и во всплывающем окне разрешите отправку вашего номера",
						replyMarkup: new ReplyKeyboardMarkup(new[]
						{
							KeyboardButton.WithRequestContact("Далее"),
							KeyboardButton.WithRequestContact("Отмена")
						}, true, true));

					var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
					var application = db.Applications.FirstOrDefault(x => x.UserId == user.UserId);
					if (application != null && user != null)
					{
						user.State = StateChatEnum.SendingNumberPhone;
						application.Time = DateTime.Now;
					}

					db.SaveChanges();
				}
				else
				{
					await bot.SendTextMessageAsync(
						msg.Chat.Id,
						text: "Вы не правильно указали время");
				}
			}
		}
	}
}