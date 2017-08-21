using SFML.Graphics;

namespace GamepadListener
{
	public struct Theme
	{
		public static Theme LightTheme { get; set; }
		public static Theme DarkTheme { get; set; }

		public static Theme SelectedTheme { get; set; }

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
