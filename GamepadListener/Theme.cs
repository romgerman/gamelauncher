using SFML.Graphics;
using System;
using System.Collections.Generic;

namespace GamepadListener
{
	public class ThemeManager
	{
		private Dictionary<string, Theme> themeStore;
		public Theme SelectedTheme { get; private set; }

		public ThemeManager()
		{
			themeStore = new Dictionary<string, Theme>();
		}

		public void SetTheme(string name)
		{
			Theme theme;

			if (themeStore.TryGetValue(name, out theme))
			{
				SelectedTheme = theme;
				Console.WriteLine($"Theme set to {name}");
			}
			else
			{
				Console.WriteLine($"Failed to set theme to {name}");
			}
		}

		public void AddTheme(string name, Theme theme)
		{
			themeStore.Add(name, theme);
		}

		public void RemoveTheme(string name)
		{
			themeStore.Remove(name);
		}

		public Theme GetTheme(string name)
		{
			return themeStore[name];
		}
	}

	public struct Theme
	{
		public Color TextColor { get; set; }
		public Color BackgroundColor { get; set; }
		public Color CursorOutlineColor { get; set; }
        public string FontName { get; set; }
        private Font font;

        public Font GetFont()
        {
            if(font == null)
            {
                font = new Font(FontName);
            }

            return font;
        }
	}
}
