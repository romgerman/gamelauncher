using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamepadListener
{
	struct Session
	{
		static private int nextSessionId = 0;

		public uint SessionGamepadId { get; set; }
		public string UserName { get; set; }
		public int Id { get; private set; }

		public Session(uint gamepadId, string userName)
		{
			SessionGamepadId = gamepadId;
			UserName = userName;

			Id = nextSessionId;
			nextSessionId++;
		}
	}
}
