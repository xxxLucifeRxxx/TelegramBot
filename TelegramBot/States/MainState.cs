using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.States
{
	public class MainState : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, State state)
		{
			if (msg.Text.Equals("привет", StringComparison.OrdinalIgnoreCase))
			{
				ReplyKeyboardMarkup ReplyKeyboard = new[]
				{
					new[] { "Плохо😒", "Хорошо😃", "Отлично😁", "Домой🏚" },
				};

				await bot.SendTextMessageAsync(
					msg.Chat.Id,
					"Hi ,men. Как дела?",
					replyMarkup: ReplyKeyboard);

				state.StateChat = StateChat.Dialog;

			}
			else
			{
				await bot.SendTextMessageAsync(chatId,
					"Я не понимаю того что вы мне написали. Введите слово привет для начала работы со мной.");
			}
		}


	}
}