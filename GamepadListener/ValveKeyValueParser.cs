using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamepadListener
{
	// TODO: This is a dirty implementation
	class ValveKeyValueParser
	{
		public struct KeyValue
		{
			public Dictionary<string, string> Values;
			public Dictionary<string, KeyValue> Keys;
		}

		public List<KeyValue> Data { get; private set; }

		public ValveKeyValueParser()
		{
			this.Data = new List<KeyValue>();
		}

		public void Parse(string str)
		{
			
		}
	}

}
