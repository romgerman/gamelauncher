﻿using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Collections.Generic;

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

	class MainMenuView : View
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

		public void init(MainClass main, RenderWindow window)
		{
			menuContainer.init(main, window);

			var textColor = new Color(0x2f2f2fff);

			timeText = new Text(DateTime.Now.ToString("t"), new Font("OpenSans-Regular.ttf"), 16);
			timeText.FillColor = Theme.SelectedTheme.TextColor;
			var rectTimeText = timeText.GetLocalBounds();
			timeText.Position = new Vector2f(window.Size.X - rectTimeText.Width - 20, 10);

			gamepadAttachedText = new Text("0", new Font("OpenSans-Regular.ttf"), 16);
			gamepadAttachedText.FillColor = Theme.SelectedTheme.TextColor;
			var rectGamepadAttachedText = gamepadAttachedText.GetGlobalBounds();
			gamepadAttachedText.Position = new Vector2f(timeText.Position.X - rectGamepadAttachedText.Width - 20, 10);

			usernameText = new Text(Environment.UserName, new Font("OpenSans-Regular.ttf"), 16);
			usernameText.FillColor = Theme.SelectedTheme.TextColor;
			usernameText.Position = new Vector2f(20, 10);

			topBarBorder = new RectangleShape(new Vector2f(window.Size.X - 20, 1.5f));
			topBarBorder.FillColor = Theme.SelectedTheme.TextColor;
			topBarBorder.Position = new Vector2f(10, 40);

			noJoystickNotice = new Text("Connect a Joystick to continue.", new Font("OpenSans-Regular.ttf"), 16);
			var noJoystickNoticeRect = noJoystickNotice.GetLocalBounds();
			noJoystickNotice.Origin = new Vector2f(noJoystickNoticeRect.Left + noJoystickNoticeRect.Width / 2.0f,
									   noJoystickNoticeRect.Top + noJoystickNoticeRect.Height / 2.0f);
			noJoystickNotice.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
			noJoystickNotice.FillColor = Theme.SelectedTheme.TextColor;

			libraryEmptyNotice = new Text("To play a game you can add a game to the library.", new Font("OpenSans-Regular.ttf"), 16);
			libraryEmptyNotice.FillColor = Theme.SelectedTheme.TextColor;
			libraryEmptyNotice.Position = new Vector2f(50, 70);

			currentElementTitleText = new Text("CURRENT_ELEMENT_TITLE", new Font("OpenSans-Regular.ttf"), 24);
			currentElementTitleText.FillColor = Theme.SelectedTheme.TextColor;
			currentElementTitleText.Position = new Vector2f(50, 70);


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

			window.JoystickButtonPressed += (sender, e) =>
			{
				var elementHasChanged = false;

				if(e.Button == 0)
				{
					menuContainer.SelectNext();
					elementHasChanged = true;
				}
				else if(e.Button == 2)
				{
					menuContainer.SelectPrev();
					elementHasChanged = true;
				}

				if (elementHasChanged)
				{
					currentElementTitleText.DisplayedString = menuContainer.GetSelectedItem().Name;
				}
			};
		}

		public void render(RenderWindow window)
		{
			timeText.Draw(window, RenderStates.Default);
			usernameText.Draw(window, RenderStates.Default);
			gamepadAttachedText.Draw(window, RenderStates.Default);
			topBarBorder.Draw(window, RenderStates.Default);

			if(!joystickConnected) noJoystickNotice.Draw(window, RenderStates.Default);

			if (menuContainer.Empty())
			{
				libraryEmptyNotice.Draw(window, RenderStates.Default);
			}

			currentElementTitleText.Draw(window, RenderStates.Default);

			menuContainer.render(window);
		}

		public void update(Window window, int dt)
		{
			timeText.DisplayedString = DateTime.Now.ToString("t");

			menuContainer.update(window, dt);
		}
	}
}
