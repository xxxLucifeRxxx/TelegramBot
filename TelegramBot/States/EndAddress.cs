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
	public class EndAddress : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, Context db)
		{
			switch (msg.Type)
			{
				case MessageType.Venue:
					case MessageType.Location:
					{
						await bot.SendTextMessageAsync(chatId,
							text: "Теперь назначьте пожалуйста время на которое желаете заказать такси. \n " +
								  "Формат для отправки времени 'hh:mm', \n" +
								  "где hh-часы, mm-минуты. " +
								  "Если хотите сделать заказ на ближайшее время, то нажмите далее",
							replyMarkup: new ReplyKeyboardMarkup(new[]
							{
							new KeyboardButton("Далее"),
							new KeyboardButton("Отмена"),
							}, true, true));

						var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
						var application = db.Applications.FirstOrDefault(x => x.UserId == user.UserId);
						if (application != null)
							if (user != null)
								user.State = StateChatEnum.SendingTime;

						if (application != null)
						{
							application.LatitudeTo = msg.Location.Latitude;
							application.LongitudeTo = msg.Location.Longitude;
						}
						db.SaveChanges();
						break;
					}
				default:
					{
						if (msg.Type != MessageType.Location)
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
								await bot.SendTextMessageAsync(
									chatId: msg.Chat.Id,
									text: "Отправленное вами сообщение не является адресом");
							}
						}
						break;
					}
			}
		}
	}
}

/*(msg.Text.Equals("Плохо😒", StringComparison.OrdinalIgnoreCase))*/