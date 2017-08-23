using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamepadListener
{
	class ValveKeyValueParser
	{
		public class KeyValue
		{
			public Dictionary<string, string> Values;
			public Dictionary<string, KeyValue> Keys;

			public KeyValue()
			{
				this.Values = new Dictionary<string, string>();
				this.Keys = new Dictionary<string, KeyValue>();
			}

			public string GetValue(string key)
			{
				string val;

				if (Values.TryGetValue(key, out val))
					return val;

				return null;
			}

			public KeyValue GetKey(string key)
			{
				KeyValue val;

				if (Keys.TryGetValue(key, out val))
					return val;

				return null;
			}
		}

		enum TokenTypes
		{
			Value,
			BraceOpen,
			BraceClose
		}

		private class Token
		{
			public TokenTypes Type;
			public string Value;
		}

		public KeyValue Data { get; private set; }

		public ValveKeyValueParser()
		{
			this.Data = new KeyValue();
		}

		private string source;
		private int lexPos;

		public void Read(string str)
		{
			this.source = str;
			this.lexPos = 0;

			Parse();
		}

		private Token LexNext() // Do not support in-line comments (will it throw and exception?)
		{
			bool readingValue = false;
			bool readingComment = false;
			string value = "";

			while (lexPos < source.Length)
			{
				if (readingComment)
				{
					if (source[lexPos] == '\n')
						readingComment = false;

					lexPos++;
					continue;
				}

				if (readingValue)
				{
					if (source[lexPos] != '"')
					{
						value += source[lexPos];
						lexPos++;
						continue;
					}
					else
					{
						readingValue = false;
						lexPos++;
						return new Token() { Type = TokenTypes.Value, Value = value };
					}
				}

				if (source[lexPos] == '"') // read value
				{
					readingValue = true;
					value = "";
					lexPos++;
				}
				else if (source[lexPos] == '{')
				{
					lexPos++;
					return new Token() { Type = TokenTypes.BraceOpen };
				}
				else if (source[lexPos] == '}')
				{
					lexPos++;
					return new Token() { Type = TokenTypes.BraceClose };
				}
				else if (source[lexPos] == '/')
				{
					if(readingValue)
					{
						value += source[lexPos];
						lexPos++;
					}
					else
					{
						readingComment = true;
						lexPos++;
					}
				}
				else if (source[lexPos] == ' ' ||
						 source[lexPos] == '\t' ||
						 source[lexPos] == '\n' ||
						 source[lexPos] == '\r')
				{
					lexPos++;
				}
			}

			return null;
		}

		private KeyValue current;

		private void Parse()
		{
			Data = new KeyValue();
			current = Data;

			Token tok = LexNext();

			while (tok != null)
			{
				switch (tok.Type)
				{
					case TokenTypes.Value:
						LexNext(); // Assume we are eating {
						ParseBlock(tok);
						tok = LexNext();
						break;
					default:
						throw new Exception("Unexpected token. File is invalid");
				}
			}
		}

		private void ParseBlock(Token name)
		{
			var token = LexNext();

			while(token.Type != TokenTypes.BraceClose)
			{
				var key = token;
				var value = LexNext();
				
				if (value.Type != TokenTypes.BraceOpen)
				{
					current.Values.Add(key.Value, value.Value);
				}
				else
				{
					var parent = current;
					current = new KeyValue();

					ParseBlock(key);

					parent.Keys.Add(key.Value, current);

					current = parent;
				}

				token = LexNext();
			}
		}
	}

}
