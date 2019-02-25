using System;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.States
{
	class SendingNumber
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, State state)
		{
			//string number;
			//Match match = Regex.Match(number, @"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$")
		}
	}
}
