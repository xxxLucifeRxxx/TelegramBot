﻿using System;
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
							text: "Укажите куда вам нужно ехать, прикрепив метку на карте к данному диалогу.");

						var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
						var application = db.Applications.FirstOrDefault(x => x.UserId == user.UserId);
						if (application == null)
						{
							if (user != null)
								db.Applications.Add(new Application()
								{
									UserId = user.UserId,
								});
							db.SaveChanges();
						}

						if (user != null)
							user.State = StateChatEnum.EndAddress;

						if (application != null)
						{
							application.LatitudeFrom = msg.Location.Latitude;
							application.LongitudeFrom = msg.Location.Longitude;
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
							if (user != null)
								user.State = StateChatEnum.StartMain;
						}
						else
						{
							await bot.SendTextMessageAsync(
								chatId: msg.Chat.Id,
								text: "Введенный текст не является вашим местоположением");
						}
						break;
					}
			}
		}
	}
}