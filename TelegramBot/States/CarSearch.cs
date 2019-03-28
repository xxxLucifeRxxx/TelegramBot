using System;
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
    class CarSearch : IUpdateState
    {
        public async void UpdateAsync(Message msg, TelegramBotClient bot, long chatId, Context db)
        {
	        switch (msg.Type)
	        {
		        case MessageType.Text when msg.Text.Equals("Отмена", StringComparison.OrdinalIgnoreCase):
		        {
			        var send = new SendMessageRequest(msg.Chat.Id,
				        "Вы отменили заказ")
			        {
				        ReplyMarkup = new ReplyKeyboardRemove(),
			        };
			        await bot.MakeRequestAsync(send);

			        var user = db.Users.FirstOrDefault(x => x.ChatId == msg.Chat.Id);
			        var application = db.Applications.FirstOrDefault(x => x.UserId == user.UserId);
			        if (application != null && user != null)
			        {
				        application.State = ClaimStatusEnum.Canceled;
				        user.State = StateChatEnum.StartMain;
			        }

			        db.SaveChanges();
			        break;
		        }
		        case MessageType.Text:
			        await bot.SendTextMessageAsync(
				        chatId: msg.Chat.Id,
				        text: "Ведется поиск водителя...");
			        break;
		        default:
			        await bot.SendTextMessageAsync(
				        chatId: msg.Chat.Id,
				        text: "Ведется поиск водителя...");
			        break;
	        }
        }
    }
}