using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramBot.Model
{
	public class User
	{
		[Key]
		public int UserId { get; set; } // ID

		public int ChatId { get; set; } // Это уникальный идентификатор чата переписки
		public string NumbPhone { get; set; } // Номер телефона получаемый на этапе составления заявки
		public int State { get; set; } // Состояния в виде отдельных классов по которым пользователь перемещается

		// Ссылка на заявки
		public virtual List<Application> Requests { get; set; }
	}

	public class Application
	{
		/// Todo: Нужно будет сделать Enum список для Application.State и для PaymentMethod

		[Key]
		public int ApplicationId { get; set; } // ID

		public int UserId { get; set; } // Поле для связи таблиц Application и User
		public int DriverId { get; set; }// Поле для связи таблиц Application и Driver
		public float LongitudeFrom { get; set; }//Получение координат откуда забрать пользователя в виде широты
		public float LatitudeFrom { get; set; }//Получение координат откуда забрать пользователя в виде долготы
		public float LongitudeTo { get; set; }//Получение координат куда ехать в виде широты
		public float LatitudeTo { get; set; }//Получение координат куда ехать в виде долготы
		public DateTime Created { get; set; }// Поле для определения времени создания заявки
		public DateTime Time { get; set; }// На этапе составления заявки пользователь указывает время на которое нужна будет машина
		public int PaymentMethod { get; set; }// На этапе составления заявки пользователь выбирает метод оплаты
		public int State { get; set; }// Определяет в каком состоянии находится заявка -Поиск водителя -Заказ принят -Заказ отменен

		// Ссылка на пользователя
		[ForeignKey(nameof(UserId))]
		public virtual User User { get; set; }

		// Ссылка на водителя
		[ForeignKey(nameof(DriverId))]
		public virtual Driver Driver { get; set; }
	}

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