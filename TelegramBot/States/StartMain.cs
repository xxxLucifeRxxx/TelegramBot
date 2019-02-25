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
			if (msg.Text.Equals("/start", StringComparison.OrdinalIgnoreCase))
			{
				var RequestReplyKeyboard = new ReplyKeyboardMarkup(new[]
				{
					KeyboardButton.WithRequestLocation("Местоположение"),
				});
				//replykeyboardmarkup replykeyboard = new[]
				//{
				//	new[] { "плохо😒", "хорошо😃", "отлично😁", "домой🏚" },

				//};

				await bot.SendTextMessageAsync(
					msg.Chat.Id,
					"Здравствуйте, откуда вас забрать?" +
					"Вы можете отправить свое местоположение нажав на кнопку," +
					" для этого в настройках включите геолокацию.",
					replyMarkup: RequestReplyKeyboard);

				if (msg.Type == MessageType.Location)
				{
					await bot.SendTextMessageAsync(chatId,
						"Укажите куда вам нужно ехать, прикрепив метку на карте к данному диалогу.");
					state.StateChat = StateChat.EndAddress;
				}
				else
				{
					await bot.SendTextMessageAsync(chatId,
						"Отправленное вами сообщение не распознается как адрес.");
					return;
				}

			}
			else
			{
				await bot.SendTextMessageAsync(chatId,
					"Для заказа такси введите команду /start");
			}
		}


	}
}