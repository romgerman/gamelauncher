using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GamepadListener
{
	class LoginView : IDrawable
	{
		Text text;
		GameLauncher launcher;
        RenderWindow window;

        private void HandleLogin(object o, JoystickButtonEventArgs ev)
        {
            if (ev.Button == 0)
            {
				launcher.Session = new Session(ev.JoystickId, Environment.UserName);
				launcher.ChangeView(new MainMenuView());
            }
        }

        private void HandleKeypress(object o, KeyEventArgs ev)
        {
            if(ev.Code == Keyboard.Key.Return)
            {
				Environment.Exit(0);
            }
        }

		public void Init(GameLauncher launcher, RenderWindow window)
		{
			text = new Text("Press A on the controller you want to use or RETURN to return to desktop.", new Font("OpenSans-Regular.ttf"), 24);
			text.FillColor = Theme.SelectedTheme.TextColor;
			var rect = text.GetLocalBounds();
			text.Position = new Vector2f(window.Size.X / 2 - rect.Width / 2.0f, window.Size.Y / 2 - rect.Height / 2.0f);

			this.launcher = launcher;
            this.window = window;

            window.JoystickButtonPressed += HandleLogin;
            window.KeyPressed += HandleKeypress;
		}

		public void Render(RenderWindow window)
		{
			text.Draw(window, RenderStates.Default);
		}

		public void Update(Window window, int dt)
		{
		}

        public void Deinit()
        {
            window.JoystickButtonPressed -= HandleLogin;
            window.KeyPressed -= HandleKeypress;
        }
    }
}
