using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GamepadListener
{
	enum PossibleGamepadButton : uint // TODO: Maybe?
	{
		// Right part of gamepad
		X = 0, // left
		A = 1, // bottom
		B = 2, // right
		Y = 3, // top
	}

	class MainMenuView : IView
	{
		Text timeText;
		Text usernameText;
		Text gamepadAttachedText;
		Text noJoystickNotice;
		Text libraryEmptyNotice;
		Text currentElementTitleText;

		RectangleShape topBarBorder;

		bool joystickConnected;
		int connectedJoystickCount;

		MenuContainer menuContainer = new MenuContainer();

        public void Deinit()
        {
        }

        public void Init(MainClass main, RenderWindow window)
		{
			menuContainer.Init(main, window);

			var font = Theme.SelectedTheme.GetFont();

			timeText = new Text(DateTime.Now.ToString("t"), font, 16);
			timeText.FillColor = Theme.SelectedTheme.TextColor;
			var rectTimeText = timeText.GetLocalBounds();
			timeText.Position = new Vector2f(window.Size.X - rectTimeText.Width - 20, 10);

			gamepadAttachedText = new Text("0", font, 16);
			gamepadAttachedText.FillColor = Theme.SelectedTheme.TextColor;
			var rectGamepadAttachedText = gamepadAttachedText.GetGlobalBounds();
			gamepadAttachedText.Position = new Vector2f(timeText.Position.X - rectGamepadAttachedText.Width - 20, 10);

			usernameText = new Text(Environment.UserName, font, 16);
			usernameText.FillColor = Theme.SelectedTheme.TextColor;
			usernameText.Position = new Vector2f(20, 10);

			topBarBorder = new RectangleShape(new Vector2f(window.Size.X - 20, 1.5f));
			topBarBorder.FillColor = Theme.SelectedTheme.TextColor;
			topBarBorder.Position = new Vector2f(10, 40);

			noJoystickNotice = new Text("Connect a Joystick to continue.", font, 16);
			var noJoystickNoticeRect = noJoystickNotice.GetLocalBounds();
			noJoystickNotice.Origin = new Vector2f(noJoystickNoticeRect.Left + noJoystickNoticeRect.Width / 2.0f,
									   noJoystickNoticeRect.Top + noJoystickNoticeRect.Height / 2.0f);
			noJoystickNotice.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
			noJoystickNotice.FillColor = Theme.SelectedTheme.TextColor;

			libraryEmptyNotice = new Text("To play a game you can add a game to the library.", font, 16);
			libraryEmptyNotice.FillColor = Theme.SelectedTheme.TextColor;
			libraryEmptyNotice.Position = new Vector2f(50, 70);

			currentElementTitleText = new Text("CURRENT_ELEMENT_TITLE", font, 24);
			currentElementTitleText.FillColor = Theme.SelectedTheme.TextColor;
			currentElementTitleText.Position = new Vector2f(50, 70);

			currentElementTitleText.DisplayedString = menuContainer.GetSelectedItem().Name;


			for (uint i = 0; i < Joystick.Count; i++)
			{
				if(Joystick.IsConnected(i))
				{
					joystickConnected = true;
					connectedJoystickCount++;
				}
			}

			gamepadAttachedText.DisplayedString = connectedJoystickCount.ToString();

			window.JoystickConnected += (sender, e) => {
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
					var current = main.library.GetWithName(menuContainer.GetSelectedItem().Name);
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
			timeText.Draw(window, RenderStates.Default);
			usernameText.Draw(window, RenderStates.Default);
			gamepadAttachedText.Draw(window, RenderStates.Default);
			topBarBorder.Draw(window, RenderStates.Default);

			if (menuContainer.Empty())
			{
				libraryEmptyNotice.Draw(window, RenderStates.Default);
			}

			currentElementTitleText.Draw(window, RenderStates.Default);

			menuContainer.Render(window);

			if (!joystickConnected) noJoystickNotice.Draw(window, RenderStates.Default);
		}

		public void Update(Window window, int dt)
		{
			timeText.DisplayedString = DateTime.Now.ToString("t");

			menuContainer.Update(window, dt);
		}
	}
}
