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
	public class StartText : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, Context db)
		{
			switch (msg.Type)
			{
				case MessageType.Location:
				{
					await bot.SendTextMessageAsync(
						chatId: msg.Chat.Id,
						text: "Нажав на иконку прикрепить, она в виде скрепки, выберите пункт геопозиция и выберите место куда вам нужно ехать.",
						replyMarkup: new ReplyKeyboardMarkup(new[]
						{
							new KeyboardButton("Отмена")
						}, true, true)); 

						var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
						var application = db.Applications.FirstOrDefault(x => x.UserId == user.UserId);
						if (application != null && application.State == ClaimStatusEnum.Canceled)
						{
							if (user != null)
								db.Applications.Add(new Application()
								{
									UserId = user.UserId,
									LatitudeFrom = msg.Location.Latitude,
									LongitudeFrom = msg.Location.Longitude
								});
							db.SaveChanges();
						}
						if (application == null)
						{
							if (user != null)
								db.Applications.Add(new Application()
								{
									UserId = user.UserId,
									LatitudeFrom  = msg.Location.Latitude,
									LongitudeFrom = msg.Location.Longitude
								});
							db.SaveChanges();
						}
						if (user != null)
							user.State = StateChatEnum.EndAddress;
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
							await bot.SendTextMessageAsync(
								chatId: msg.Chat.Id,
								text: "Введенный текст не является вашим местоположением, для его определения нажмите на кнопку Далее.");
						}
						break;
					}
			}
		}
	}
}