using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TelegramBot.Model
{
	public class User
	{
		[Key]
		public int UserId { get; set; } // ID

		public long ChatId { get; set; } // Это уникальный идентификатор чата переписки
		public string NumbPhone { get; set; } // Номер телефона получаемый на этапе составления заявки
		public string State { get; set; } // Состояния в виде отдельных классов по которым пользователь перемещается

		// Ссылка на заявки
		public virtual List<Application> Requests { get; set; }
	}
}