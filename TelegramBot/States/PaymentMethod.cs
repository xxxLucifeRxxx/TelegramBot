using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Enumerations;

namespace TelegramBot.States
{
	class PaymentMethod : IUpdateState
	{
		public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, State state)
		{
		    var keyboardCashPayment = new InlineKeyboardMarkup(new[]
		    {

		        new[]
		        {
		            InlineKeyboardButton.WithCallbackData("Наличными", Globals.CallbackCancel),

		        }
		    });

		    var keyboardMobileBank = new InlineKeyboardMarkup(new[]
		    {

		        new[]
		        {
		            InlineKeyboardButton.WithCallbackData("Мобильный банк", Globals.CallbackCancel),

		        }
		    });

            if (msg.Type != MessageType.Contact)
		    {
		        await bot.SendTextMessageAsync(
		            chatId: msg.Chat.Id,
		            text: "Отправленное сообщение не является вашим номером телефона");
		    }
		    else
		    {
		        var send = new SendMessageRequest(msg.Chat.Id,
		            "Выберите способ оплаты")
		        {
		            ReplyMarkup = new ReplyKeyboardRemove(),
		        };

		        await bot.MakeRequestAsync(send);

		        await bot.SendTextMessageAsync(
		            chatId: msg.Chat.Id,
		            text: "Можно произвести оплату:",
		            replyMarkup:keyboardCashPayment);

		        await bot.SendTextMessageAsync(
		            chatId: msg.Chat.Id,
		            text: "или через",
		            replyMarkup: keyboardMobileBank);

                state.StateChatEnum = StateChatEnum.EndAddress;
		    }

        }
	}
}
