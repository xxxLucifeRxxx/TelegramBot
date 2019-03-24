using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Enumerations;
using TelegramBot.Model;

namespace TelegramBot.States
{
    public class Time : IUpdateState
    {
        public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, Context db)
        {
            var requestReplyKeyboard = new ReplyKeyboardMarkup(new[]
            {
                KeyboardButton.WithRequestContact("Отправить текущий номер"),
            }, true, true);

            await bot.SendTextMessageAsync(
                msg.Chat.Id,
                text: "Текущее время отправлено \n" +
                      "Теперь укажите пожалуйста ваш номер телефона.",
                replyMarkup: requestReplyKeyboard);

			var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
			if (user != null)
				user.State = StateChatEnum.PaymentMethod;
		}
    }
}