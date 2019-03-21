using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Model;
using User = Telegram.Bot.Types.User;

namespace TelegramBot.States
{
	internal interface IUpdateState
	{
		void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, Context db);
	}
}