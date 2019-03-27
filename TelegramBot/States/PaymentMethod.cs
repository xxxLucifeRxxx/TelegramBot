using System;
using Telegram.Bot;
using Telegram.Bot.Requests;
using System.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Enumerations;
using TelegramBot.Model;

namespace TelegramBot.States
{
	internal class PaymentMethod : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, Context db)
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
			else
			{
				if (msg.Text.Equals("Мобильный банк", StringComparison.OrdinalIgnoreCase))
				{
					await bot.SendTextMessageAsync(
						chatId: msg.Chat.Id,
						text: "Вы выбрали способ оплаты через мобильный банк",
						replyMarkup: new ReplyKeyboardMarkup(new[]
						{
							new KeyboardButton("Отмена")
						}, true, true));

					var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
					var application = db.Applications.FirstOrDefault(x => x.UserId == user.UserId);
					if (application != null)
					{
						application.State = ClaimStatusEnum.Searching;
						application.Created = DateTime.Now;
						application.PaymentMethod = PaymentMethodEnum.MobileBank;
						application.State = ClaimStatusEnum.Searching;
						db.SaveChanges();
					}

					if (user != null)
						user.State = StateChatEnum.CarSearch;
					db.SaveChanges();
				}
				else if (msg.Text.Equals("Наличные", StringComparison.OrdinalIgnoreCase))
				{
					await bot.SendTextMessageAsync(
						chatId: msg.Chat.Id,
						text: "Вы выбрали способ оплаты наличными",
						replyMarkup: new ReplyKeyboardMarkup(new[]
						{
							new KeyboardButton("Отмена")
						}, true, true));

					var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
					var application = db.Applications.FirstOrDefault(x => x.UserId == user.UserId);
					if (application != null)
					{
						application.State = ClaimStatusEnum.Searching;
						application.Created = DateTime.Now;
						application.PaymentMethod = PaymentMethodEnum.Cash;
						application.State = ClaimStatusEnum.Searching;
						db.SaveChanges();
					}

					if (user != null)
						user.State = StateChatEnum.CarSearch;
					db.SaveChanges();
				}
				else
				{
					await bot.SendTextMessageAsync(
						chatId: msg.Chat.Id,
						text: "Такой способ оплаты не предусмотрен");
				}
			}
		}
	}
}