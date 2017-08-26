using System;

using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace GamepadListener.UI
{
	/// <summary>
	/// Text object
	/// </summary>
	class UIText : IUIElement
	{
		private Text _text;

		public string Text
		{
			get { return _text.DisplayedString; }
			set { _text.DisplayedString = value; }
		}

		public uint FontSize
		{
			get { return _text.CharacterSize; }
			set { _text.CharacterSize = value; }
		}

		public Color ForegroundColor
		{
			get { return _text.FillColor; }
			set { _text.FillColor = value; }
		}

		public Vector2f Offset
		{
			get { return _offset; }
			set
			{
				_offset = value;
				_text.Position = _offset;
			}
		}

		private Vector2f _offset;

		public FloatRect Bounds => _text.GetLocalBounds();

		public IUIElement Parent
		{
			get { return _parent; }
			private set { _parent = value; }
		}

		private IUIElement _parent;

		public UIText()
		{
			_text = new Text("", Theme.SelectedTheme.GetFont());
		}

		public UIText(string text)
		{
			_text = new Text(text, Theme.SelectedTheme.GetFont());
		}

		#region IDrawable interfaces

		public void Init(GameLauncher launcher, RenderWindow window)
		{
			
		}

		public void Deinit()
		{
			_text.Dispose();
		}

		public void Render(RenderWindow window)
		{
			_text.Draw(window, RenderStates.Default);
		}

		public void Update(Window window, int dt)
		{
			
		}

		#endregion
	}

}
