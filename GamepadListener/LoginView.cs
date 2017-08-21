using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GamepadListener
{
	class LoginView : View
	{
		Text text;
		MainClass main;

        private void HandleLogin(object o, JoystickButtonEventArgs ev)
        {
            if (ev.Button == 0)
            {
                main.pendingView = new MainMenuView();
            }
        }

        private void HandleKeypress(object o, KeyEventArgs ev)
        {
            if(ev.Code == Keyboard.Key.Return)
            {
                Environment.Exit(0);
            }
        }

		public void init(MainClass main, RenderWindow window)
		{
			text = new Text("Press A to use the controller or RETURN to return to desktop.", new Font("OpenSans-Regular.ttf"), 24);
			text.FillColor = Theme.SelectedTheme.TextColor;
			var rect = text.GetLocalBounds();
			text.Position = new Vector2f(window.Size.X / 2 - rect.Width / 2.0f, window.Size.Y / 2 - rect.Height / 2.0f);

			this.main = main;

            window.JoystickButtonPressed += HandleLogin;
            window.KeyPressed += HandleKeypress;
		}

		public void render(RenderWindow window)
		{
			text.Draw(window, RenderStates.Default);
		}

		public void update(Window window, int dt)
		{
		}
	}
}
