using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Enumerations;
using TelegramBot.Model;

namespace TelegramBot.States
{
	public class Cancel : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, Context db)
		{
			var send = new SendMessageRequest(msg.Chat.Id,
				"Вы отменили заказ")
			{
				ReplyMarkup = new ReplyKeyboardRemove(),
			};
			await bot.MakeRequestAsync(send);

			var user = db.Users.FirstOrDefault(x => x.ChatId == chatId);
			if (user != null)
				user.State = StateChatEnum.StartMain;
		}
	}
}