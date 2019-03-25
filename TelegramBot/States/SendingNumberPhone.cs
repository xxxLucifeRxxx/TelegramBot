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
							"Теперь выберите пожалуйста способ оплаты",
							replyMarkup: new ReplyKeyboardMarkup(new[]
							{
							new KeyboardButton("Наличные"),
							new KeyboardButton("Мобильный банк"),
							}, true, true));

						var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
						var application = db.Applications.FirstOrDefault(x => x.UserId == user.UserId);
						if (application != null)
							application.NumbPhone = msg.Contact.PhoneNumber;
						if (user != null)
							user.State = StateChatEnum.PaymentMethod;

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
							if (user != null)
								user.State = StateChatEnum.StartMain;
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