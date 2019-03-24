using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Model;

namespace TelegramBot.States
{
	internal interface IUpdateState
	{
		void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, Context db);
	}
}