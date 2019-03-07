using System;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.States
{
	public class StartText : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, State state)
		{
			if (msg.Type != MessageType.Location)
			{
				await bot.SendTextMessageAsync(
					chatId: msg.Chat.Id,
					text: "Введенный текст не является местоположением");
			}
			else
			{
				var send = new SendMessageRequest(msg.Chat.Id,
					"Укажите куда вам нужно ехать, прикрепив метку на карте к данному диалогу.")
				{
					ReplyMarkup = new ReplyKeyboardRemove(),
				};
				await bot.MakeRequestAsync(send);

				state.StateChat = StateChat.EndAddress;
			}
		}
	}
}