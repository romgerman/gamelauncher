using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace GamepadListener.UI
{
	// TODO: maybe we need to make layouts or something too?
	// 

	class UIButton : IView
	{
		private RectangleShape _background;
		private Text _title;

		public UIButton(Vector2f position, string text = "", float width = 0f, float height = 0f)
		{
			_background = new RectangleShape(new Vector2f(width, height));
			_background.Position = position;
			_title = new Text(text, Theme.SelectedTheme.GetFont());
		}

		public void SetText(string text)
		{
			_title.DisplayedString = text;
		}

		public void SetSizeRelativeToText()
		{

		}

		public void Init(MainClass main, RenderWindow window)
		{
			
		}

		public void Deinit()
		{
			throw new NotImplementedException();
		}

		public void Render(RenderWindow window)
		{
			throw new NotImplementedException();
		}

		public void Update(Window window, int dt)
		{
			throw new NotImplementedException();
		}
	}
}
