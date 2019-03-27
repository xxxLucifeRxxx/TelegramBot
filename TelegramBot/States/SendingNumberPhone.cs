using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Enumerations;
using TelegramBot.Model;

namespace TelegramBot.States
{
	public class SendingNumberPhone : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, Context db)
		{
			switch (msg.Type)
			{
				case MessageType.Contact:
					{
						await bot.SendTextMessageAsync(chatId,
							"Теперь выберите пожалуйста способ оплаты, после выбора способа оплаты начнется поиск водителя.",
							replyMarkup: new ReplyKeyboardMarkup(new[]
							{
							new KeyboardButton("Наличные"),
							new KeyboardButton("Мобильный банк"),
							new KeyboardButton("Отмена")
							}, true, true));

						var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
						var application = db.Applications.FirstOrDefault(x => x.UserId == user.UserId);
						if (application != null && user != null)
						{
							application.NumbPhone = msg.Contact.PhoneNumber;
							user.State = StateChatEnum.PaymentMethod;
						}
						db.SaveChanges();
						break;
					}
				default:
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
							await bot.SendTextMessageAsync(chatId,
								"Вы отправили неверный номер телефона");
						}
						break;
					}
			}
		}
	}
}