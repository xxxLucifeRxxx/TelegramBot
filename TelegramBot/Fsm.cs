using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
		public Fsm(long chatId, List<State> states, Message msg, TelegramBotClient bot)
		{
			var db = new Context();
			User user = new User
			{
				ChatId = chatId,
				State = StateChatEnum.StartText.ToString()
			};

			var state = states.FirstOrDefault(x => x.ChatId == chatId);
			if (state == null)
			{
				state = new State() { ChatId = chatId, StateChatEnum = StateChatEnum.StartMain };
				states.Add(state);
			}

			IUpdateState updateState;
			switch (state.StateChatEnum)
			{
				case StateChatEnum.StartMain:
					if (db.Users.Any(x => x.ChatId != chatId))
					{
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
			updateState.UpdateAsync(msg, bot, chatId, state);
		}
	}
}