using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.States
{
	public class StartMain : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, State state)
		{
			switch (msg.Type)
			{
				case MessageType.Text when msg.Text.Equals("/start", StringComparison.OrdinalIgnoreCase):
					{
						var keyboard = new InlineKeyboardMarkup(new[]
						{
						new[]
						{
							InlineKeyboardButton.WithCallbackData("Местонахождение", MyBot.CallbackLocation),
						}
					});

						////var RequestReplyKeyboard = new ReplyKeyboardMarkup(new[]
						////{
						////	KeyboardButton.WithRequestLocation("Местоположение"),
						////});
						//replykeyboardmarkup replykeyboard = new[]
						//{
						//	new[] { "плохо😒", "хорошо😃", "отлично😁", "домой🏚" },

						//};

						await bot.SendTextMessageAsync(
							chatId: msg.Chat.Id,
							text: "`Здравствуйте, откуда вас забрать? \n" +
								  "Для определения вашего местонахождения " +
								  "в настройках телефона включите` *геолокацию.*",
							replyMarkup: keyboard,
							parseMode: ParseMode.Markdown);

						return;
					}
				case MessageType.Text:
					await bot.SendTextMessageAsync(
						chatId: msg.Chat.Id,
						text: "Для заказа такси введите команду /start");
					break;

				case MessageType.Location:
					await bot.SendTextMessageAsync(
						chatId: msg.Chat.Id,
						text: "Укажите куда вам нужно ехать, прикрепив метку на карте к данному диалогу.");

					state.StateChat = StateChat.EndAddress;
					break;
			}
		}
	}
}