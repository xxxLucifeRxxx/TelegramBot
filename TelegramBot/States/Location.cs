using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.States
{
	public partial class Location : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, State state)
		{
			var RequestReplyKeyboard = new ReplyKeyboardMarkup(new[]
			{
				KeyboardButton.WithRequestLocation("Местонахождение"),
				KeyboardButton.WithRequestContact("Контакт"),
			});


			await bot.SendTextMessageAsync(
				chatId,
				"Я вижу где ты находишься. Ладно шучу, сам я не могу получить твои данные, но ты можешь мне их отправить)",
				replyMarkup: RequestReplyKeyboard);
		}
	}
}