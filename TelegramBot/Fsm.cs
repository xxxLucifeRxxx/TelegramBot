using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Enumerations;
using TelegramBot.States;
using Context = TelegramBot.Model.Context;
using User = TelegramBot.Model.User;

namespace TelegramBot
{
	public class Fsm
	{
		///Todo: Нужно заменить List<State> на Context
		public Fsm(long chatId, Message msg, TelegramBotClient bot, Context db)
		{
			var state = db.Users.FirstOrDefault(x => x.ChatId == chatId);
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

			IUpdateState updateState;
			if (state != null)
			{
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

					case StateChatEnum.Cancel:
						updateState = new Cancel();
						break;

					case StateChatEnum.SendingTime:
						updateState = new SendingTime();
						break;

					case StateChatEnum.PaymentMethod:
						updateState = new PaymentMethod();
						break;

					case StateChatEnum.Time:
						updateState = new Time();
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