using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Enumerations;

namespace TelegramBot.States
{
	public class EndAddress : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, State state)
		{
            //await bot.SendChatActionAsync(msg.Chat.Id, ChatAction.FindLocation);
		    

		    var keyboard = new InlineKeyboardMarkup(new[]
		    {
		        new[]
		        {
		            InlineKeyboardButton.WithCallbackData("Указать текущее время", Globals.CallbackTime),
		        }
		    });

            if (msg.Type != MessageType.Venue)
			{
			    await bot.SendTextMessageAsync(
			        chatId: msg.Chat.Id,
			        text: "Отправленное сообщение не является адресом");
            }
			else
			{
			    await bot.SendTextMessageAsync(chatId,
			        text: "Теперь назначьте пожалуйста время на которое желаете заказать такси. \n " +
			              "Формат для отправки времени 'hh:mm \n" +
			              "Где hh-часы, mm-минуты'",
			        replyMarkup: keyboard);
			    state.StateChatEnum = StateChatEnum.SendingTime;

            }
		}
	}
}

/*(msg.Text.Equals("Плохо😒", StringComparison.OrdinalIgnoreCase))*/