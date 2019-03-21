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
					var requestReplyKeyboard = new ReplyKeyboardMarkup(new[]
					{
							KeyboardButton.WithRequestLocation("Местоположение"),
					}, true, true);

					var keyboard = new InlineKeyboardMarkup(new[]
					{
						new[]
						{
							InlineKeyboardButton.WithCallbackData("Отмена", Globals.CallbackCancel),
						}
					});

					await bot.SendTextMessageAsync(
						chatId: msg.Chat.Id,
						text: "`Здравствуйте, откуда вас забрать? \n" +
							  "Для определения вашего местонахождения " +
							  "в настройках телефона включите` *геолокацию.*",
						replyMarkup: requestReplyKeyboard,
						parseMode: ParseMode.Markdown);

					await bot.SendTextMessageAsync(
						chatId: msg.Chat.Id,
						text: "Или чтобы отменить заказ нажмите Отмена ↓↓↓",
						replyMarkup: keyboard);

					var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
					if (user != null)
						user.State = StateChatEnum.EndAddress;

					break;

				case MessageType.Text:
					await bot.SendTextMessageAsync(
						chatId: msg.Chat.Id,
						text: "Для заказа такси введите команду \n /order");
					break;
			}
		}
	}
}