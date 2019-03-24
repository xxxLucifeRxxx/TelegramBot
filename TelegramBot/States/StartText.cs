using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Enumerations;
using TelegramBot.Model;

namespace TelegramBot.States
{
	public class StartText : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, Context db)
		{
			if (msg.Type == MessageType.Location)
			{
				var send = new SendMessageRequest(msg.Chat.Id,
					"Укажите куда вам нужно ехать, прикрепив метку на карте к данному диалогу.");
				//{
				//	ReplyMarkup = new ReplyKeyboardRemove(),
				//};

				await bot.MakeRequestAsync(send);

				var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
				if (user != null)
					user.State = StateChatEnum.EndAddress;
			}
			else
			{
				await bot.SendTextMessageAsync(
					chatId: msg.Chat.Id,
					text: "Введенный текст не является вашим местоположением");
			}
		}
	}
}