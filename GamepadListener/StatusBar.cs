using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;
using SFML.Window;

using GamepadListener.UI;
using GamepadListener.UI.Layouts;

namespace GamepadListener
{
	// TODO: this is to separate main screen bullshit. So top - status bar, bottom - interactable area

	class StatusBar : IDrawable
	{
		// username ______________________________________________ 12:56

		UIView _container;
		UIText _username;
		UIText _clock;

		RectangleShape _bottomLine;

		public StatusBar()
		{
			_container = new UIView();
			_username = new UIText("username");
			_clock = new UIText("00:00");
			
			_username.ForegroundColor = _clock.ForegroundColor = Color.Black;
			_username.FontSize = _clock.FontSize = 16;

			_container.Add(_username);
			_container.Add(_clock);

			_bottomLine = new RectangleShape();
			_bottomLine.FillColor = Color.Black;
		}

		public void SetUsername(string name)
		{
			_username.Text = name;
		}

		#region IDrawable interface

		public void Init(GameLauncher launcher, RenderWindow window)
		{
			_container.Bounds = new FloatRect(0.0f, 0.0f, window.Size.X, 40.0f);
			_container.Init(launcher, window);

			_username.HorizontalAlingment = UIAlignmentHorizontal.Left; // TODO: implement alingment so it can be set even if element is not initialized
			_clock.HorizontalAlingment = UIAlignmentHorizontal.Right;
			_username.VerticalAlingment = _clock.VerticalAlingment = UIAlignmentVertical.Middle;

			_bottomLine.Size = new Vector2f(window.Size.X - 20, 1.5f);
			_bottomLine.Position = new Vector2f(10, 40);
		}

		public void Deinit()
		{
			_container.Deinit();
		}

		public void Render(RenderWindow window)
		{
			_container.Render(window);
			_bottomLine.Draw(window, RenderStates.Default);
		}

		public void Update(Window window, int dt)
		{
			_clock.Text = DateTime.Now.ToString("t");
			_container.Update(window, dt);
		}

		#endregion
	}
}
