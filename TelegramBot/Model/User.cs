using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TelegramBot.Enumerations;

namespace TelegramBot.Model
{
	public class User
	{
		[Key]
		public int UserId { get; set; } // ID

		public long ChatId { get; set; } // Это уникальный идентификатор чата переписки

		public StateChatEnum State { get; set; } // Состояния в виде отдельных классов по которым пользователь перемещается

		// Ссылка на заявки
		public virtual List<Application> Requests { get; set; }
	}
}