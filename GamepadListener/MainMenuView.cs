using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GamepadListener
{
	class MainMenuView : IDrawable
	{
		// Text gamepadAttachedText;
		Text noJoystickNotice;
		Text libraryEmptyNotice;
		Text currentElementTitleText;

		bool joystickConnected;
		// int connectedJoystickCount;

		MenuContainer menuContainer = new MenuContainer();
		StatusBar statusBar = new StatusBar();

        public void Deinit()
        {
        }

        public void Init(GameLauncher launcher, RenderWindow window)
		{
			menuContainer.Init(launcher, window);
			statusBar.Init(launcher, window);

			var font = launcher.ThemeManager.SelectedTheme.GetFont();

			statusBar.SetUsername(launcher.Session.UserName);

			noJoystickNotice = new Text("Connect a Joystick to continue.", font, 16);
			var noJoystickNoticeRect = noJoystickNotice.GetLocalBounds();
			noJoystickNotice.Origin = new Vector2f(noJoystickNoticeRect.Left + noJoystickNoticeRect.Width / 2.0f,
									   noJoystickNoticeRect.Top + noJoystickNoticeRect.Height / 2.0f);
			noJoystickNotice.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
			noJoystickNotice.FillColor = launcher.ThemeManager.SelectedTheme.TextColor;

			libraryEmptyNotice = new Text("To play a game you can add a game to the library.", font, 16);
			libraryEmptyNotice.FillColor = launcher.ThemeManager.SelectedTheme.TextColor;
			libraryEmptyNotice.Position = new Vector2f(50, 70);

			currentElementTitleText = new Text("CURRENT_ELEMENT_TITLE", font, 24);
			currentElementTitleText.FillColor = launcher.ThemeManager.SelectedTheme.TextColor;
			currentElementTitleText.Position = new Vector2f(50, 70);

			currentElementTitleText.DisplayedString = menuContainer.GetSelectedItem().Name;


			/*for (uint i = 0; i < Joystick.Count; i++)
			{
				if(Joystick.IsConnected(i))
				{
					joystickConnected = true;
					connectedJoystickCount++;
				}
			}*/

			// gamepadAttachedText.DisplayedString = connectedJoystickCount.ToString();

			/*window.JoystickConnected += (sender, e) => {
				joystickConnected = true;
				connectedJoystickCount++;

				gamepadAttachedText.DisplayedString = connectedJoystickCount.ToString();
			};

			window.JoystickDisconnected += (sender, e) => {
				if(connectedJoystickCount > 0)
				{
					connectedJoystickCount--;
					joystickConnected &= connectedJoystickCount != 0;
				}

				gamepadAttachedText.DisplayedString = connectedJoystickCount.ToString();
			};*/

			joystickConnected = Joystick.IsConnected(launcher.Session.SessionGamepadId);

			window.JoystickConnected += (sender, e) =>
			{
				if(e.JoystickId == launcher.Session.SessionGamepadId)
				{
					joystickConnected = true;
				}
			};

			window.JoystickDisconnected += (sender, e) =>
			{
				if(e.JoystickId == launcher.Session.SessionGamepadId)
				{
					joystickConnected = false;
				}
			};

			window.JoystickMoved += (sender, e) =>
			{
				if(e.Axis == Joystick.Axis.PovX)
				{
					var elementHasChanged = false;

					if(e.Position < 0)
					{
						menuContainer.SelectPrev();
						elementHasChanged = true;

					}
					else if(e.Position > 0)
					{
						menuContainer.SelectNext();
						elementHasChanged = true;
					}

					if (elementHasChanged)
					{
						currentElementTitleText.DisplayedString = menuContainer.GetSelectedItem().Name;
					}
				}
			};

			window.JoystickButtonPressed += (sender, e) =>
			{
				// 0 = A button
				if(e.Button == 0)
				{
					var current = launcher.Library.GetWithName(menuContainer.GetSelectedItem().Name);
					if(!String.IsNullOrEmpty(current.Path))
					{
						Process proc = new Process();
						proc.StartInfo.FileName = current.Path;
						proc.Start();
						proc.WaitForExit();
						Console.WriteLine("Process exited with exit code {0}", proc.ExitCode);
					}
					else
					{
						Console.WriteLine("Current element '{0}' has no path.", current);
					}
				}
			};
		}

		public void Render(RenderWindow window)
		{
			// gamepadAttachedText.Draw(window, RenderStates.Default);

			if (menuContainer.Empty())
			{
				libraryEmptyNotice.Draw(window, RenderStates.Default);
			}

			currentElementTitleText.Draw(window, RenderStates.Default);

			menuContainer.Render(window);
			statusBar.Render(window);

			if (!joystickConnected) noJoystickNotice.Draw(window, RenderStates.Default);

			
		}

		public void Update(Window window, int dt)
		{
			menuContainer.Update(window, dt);
			statusBar.Update(window, dt);
		}
	}
}
