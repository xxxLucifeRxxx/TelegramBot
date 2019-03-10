using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.States
{
	public class Cancel : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, State state)
		{

			await bot.AnswerCallbackQueryAsync(
				callbackQueryId: Globals.Data,
				"Заказ отменен",
				true, null, 30);

			var send = new SendMessageRequest(msg.Chat.Id,
				"Заказ отменен")
			{
				ReplyMarkup = new ReplyKeyboardRemove(),
			};
			await bot.MakeRequestAsync(send);

			state.StateChat = StateChat.StartMain;
		}
	}
}