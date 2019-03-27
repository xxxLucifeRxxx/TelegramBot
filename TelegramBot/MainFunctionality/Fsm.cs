using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Enumerations;
using TelegramBot.States;
using Context = TelegramBot.Model.Context;
using User = TelegramBot.Model.User;

namespace TelegramBot.MainFunctionality
{
	public class Fsm
	{
		public  static readonly object Locker  = new object();
		public Fsm(long chatId, Message msg, TelegramBotClient bot, Context db)
		{
			User state;

			lock (Locker)
			{
			  state = db.Users.FirstOrDefault(x => x.ChatId == chatId);
			}

			if (state == null)
			{
				User user = new User
				{
					ChatId = chatId,
					State = StateChatEnum.StartMain
				};
				db.Users.Add(user);
				db.SaveChanges();
			}

			if (state != null)
			{
				IUpdateState updateState;

				switch (state.State)
				{
					case StateChatEnum.StartMain:
						var uid = db.Users.FirstOrDefault(x => x.ChatId == chatId);
						if (uid == null)
						{
							User user = new User
							{
								ChatId = chatId,
								State = StateChatEnum.StartText
							};

							db.Users.Add(user);
							db.SaveChanges();
						}

						updateState = new StartMain();
						break;

					case StateChatEnum.EndAddress:
						updateState = new EndAddress();
						break;

					case StateChatEnum.StartText:
						updateState = new StartText();
						break;

					case StateChatEnum.SendingTime:
						updateState = new SendingTime();
						break;

					case StateChatEnum.SendingNumberPhone:
						updateState = new SendingNumberPhone();
						break;

					case StateChatEnum.PaymentMethod:
						updateState = new PaymentMethod();
						break;

					case StateChatEnum.CarSearch:
						updateState = new CarSearch();
						break;

					//case StateChatEnum.CarSearch:
					//	updateState = new CarSearch();
					//	break;

					default:
						throw new AggregateException();
				}

				updateState.UpdateAsync(msg, bot, chatId, db);
			}
		}
	}
}