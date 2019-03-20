using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TelegramBot.Model
{
	public class Driver
	{
		[Key]
		public int DriverId { get; set; }// ID

		public string FullName { get; set; }// ФИО водителя
		public string NumberCar { get; set; }// Номер автомобиля

		// Ссылка на заявки
		public virtual List<Application> Requests { get; set; }
	}
}