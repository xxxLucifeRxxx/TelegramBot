using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.States
{
	public class EndAddress : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, State state)
		{
			if (msg.Type == MessageType.Location)
			{
				await bot.SendTextMessageAsync(chatId, "Теперь назначьте пожалуйста время на которое желаете заказать такси. \n " +
													   "Тут есть кнопка для отправки текущего времени, также вы сами можете вписать время в виде текста");
				state.StateChat = StateChat.Time;
			}
			else if (msg.Type == MessageType.Text)
			{
				msg.Text = msg.Location.ToString();
			}
			else
			{
				await bot.SendTextMessageAsync(chatId, "Отправленное вами сообщение не распознается как адрес.");
			}
		}
	}
}

/*(msg.Text.Equals("Плохо😒", StringComparison.OrdinalIgnoreCase))*/