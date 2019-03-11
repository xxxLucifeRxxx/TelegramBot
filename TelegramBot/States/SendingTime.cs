using System;
using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.Text.RegularExpressions;

namespace TelegramBot.States
{
	public partial class SendingTime : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, State state)
		{
			var time = DateTime.Now.ToShortTimeString();
			var keyboard = new InlineKeyboardMarkup(new[]
			{
				new[]
				{
					InlineKeyboardButton.WithCallbackData("Указать текущее время", Globals.CallbackTime),
				}
			});

			if (msg.Text.Equals("Текущее время", StringComparison.OrdinalIgnoreCase))
			{
				await bot.SendTextMessageAsync(chatId,
					time + "\n Укажите пожалуйста ваш номер телефона, также вы можете нажать кнопку для отправки вашего текущего номера телефона.");
			}
		}
	}

	public class Templates
	{
		public static void TemplatesTime()
		{
			public static string[] timeTemplate = { "00:59" };

		private Regex rgx = new Regex(@"^[00-24, 00-59]");
	}
}
}