using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
	public class State
	{
		public long ChatId { get; set; }
		public StateChat StateChat { get; set; }
	}
}