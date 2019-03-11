using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.States;

namespace TelegramBot
{
	public class Fsm
	{
		public Fsm(long chatId, List<State> states, Message msg, TelegramBotClient bot)
		{
			var state = states.FirstOrDefault(x => x.ChatId == chatId);
			if (state == null)
			{
				state = new State() { ChatId = chatId, StateChat = StateChat.StartMain };
				states.Add(state);
			}

			IUpdateState updateState;
			switch (state.StateChat)
			{
				case StateChat.StartMain:
					updateState = new StartMain();
					break;

				case StateChat.EndAddress:
					updateState = new EndAddress();
					break;

				case StateChat.Time:
					updateState = new SendingTime();
					break;

				case StateChat.StartText:
					updateState = new StartText();
					break;

				case StateChat.Cancel:
					updateState = new Cancel();
					break;

				case StateChat.SendingTime:
					updateState = new SendingTime();
					break;

				//case StateChat.Number:
				// updateState = new SendingNumber();
				// break;

				default:
					throw new AggregateException();
			}
			updateState.UpdateAsync(msg, bot, chatId, state);
		}
	}
}