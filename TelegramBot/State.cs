using TelegramBot.Enumerations;

namespace TelegramBot
{
	public class State
	{
		public long ChatId { get; set; }
		public StateChatEnum StateChatEnum { get; set; }
	}
}