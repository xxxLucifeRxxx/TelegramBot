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
                state = new State() { ChatId = chatId, StateChat = StateChat.Main };  
                states.Add(state);
            }

            IUpdateState updateState;
            switch (state.StateChat)
            {
                case StateChat.Main:
                    updateState = new MainState();
                    break;

                case StateChat.Dialog:
                    updateState = new Dialog();
                    break;

				case StateChat.Location:
					updateState = new States.Location();
					break;

                default:
                    throw new AggregateException();
            }
            updateState.UpdateAsync(msg, bot, chatId, state);
        }
    }
}