using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.States
{
	public partial class SendingTime : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, State state)
		{
			var time = DateTime.Now.ToShortTimeString();
			var RequestReplyKeyboard = new ReplyKeyboardMarkup(new[]
			{
				new KeyboardButton("Текущее время")
			});

			if (msg.Text.Equals("Текущее время", StringComparison.OrdinalIgnoreCase))
			{
				await bot.SendTextMessageAsync(chatId,
					time+ "\n Укажите пожалуйста ваш номер телефона, также вы можете нажать кнопку для отправки вашего текущего номера телефона.");
			}

			
		}
	}
}