using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Enumerations;
using Context = TelegramBot.Model.Context;

namespace TelegramBot.States
{
	public class StartMain : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, Context db)
		{
			switch (msg.Type)
			{
				case MessageType.Text when msg.Text.Equals("/order", StringComparison.OrdinalIgnoreCase):
					{
						await bot.SendTextMessageAsync(
							chatId: msg.Chat.Id,
							text: "`Здравствуйте, откуда вас забрать? \n" +
								  "Для определения вашего местонахождения " +
								  "в настройках телефона включите` *геолокацию* \n" +
								  "и нажмите на кнопку Далее.",
							replyMarkup: new ReplyKeyboardMarkup(new[]
							{
								KeyboardButton.WithRequestLocation("Далее"),
								new KeyboardButton("Отмена"),
							}, true, true),
							parseMode: ParseMode.Markdown);

						var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
						if (user != null)
							user.State = StateChatEnum.StartText;
						db.SaveChanges();
						break;
					}

				case MessageType.Text:
					{
						await bot.SendTextMessageAsync(
							chatId: msg.Chat.Id,
							text: "Для заказа такси введите команду \n /order");
						break;
					}
			}
		}
	}
}