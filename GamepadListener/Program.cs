using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using GamepadListener;
using System.IO;

class MainClass
{
	public GamepadListener.IDrawable currentView;
	public GamepadListener.IDrawable pendingView;
    public int sessionJoystickId;
	public Library library;

	void PopulateLibraryWithGameCollections()
	{
		SteamCollection steam = new SteamCollection();
		steam.FetchGameList();
		OriginCollection origin = new OriginCollection();
		origin.FetchGameList();
		UplayCollection uplay = new UplayCollection();
		uplay.FetchGameList();

		foreach (var g in steam.Games)
		{
			if (!library.HasWithName(g.Name))
			{
				library.AddItem(new LibraryItemApplication()
				{
					Name = g.Name,
				});
			}
		}

		foreach (var g in origin.Games)
		{
			if (!library.HasWithName(g.Name))
			{
				library.AddItem(new LibraryItemApplication()
				{
					Name = g.Name,
				});
			}
		}

		foreach (var g in uplay.Games)
		{
			if (!library.HasWithName(g.Name))
			{
				library.AddItem(new LibraryItemApplication()
				{
					Name = g.Name,
				});
			}
		}
	}

	static void Main(string[] args)
	{
		var window = new RenderWindow(new VideoMode(1360, 768, 32), "launcher");

		Joystick.Update();

		var id = Joystick.GetIdentification(0);
		Console.WriteLine("0: {0}, 1: {1}, 2: {2}, 3: {3}", Joystick.IsConnected(0), Joystick.IsConnected(1), Joystick.IsConnected(2), Joystick.IsConnected(3));
		Console.WriteLine("VendorID: {0} Product ID: {1}", id.VendorId, id.ProductId);
		var controller_name = "Joystick Use: " + id.Name;

		if(Joystick.IsConnected(0))
		{
			uint buttonCount = Joystick.GetButtonCount(0);

			bool hasZ = Joystick.HasAxis(0, Joystick.Axis.Z);

			Console.WriteLine("Button Count: {0}", buttonCount);
			Console.WriteLine("Has a z-axis: {0}", hasZ);
		}

		var tickClock = new Clock();
		var timeSinceLastUpdate = Time.Zero;
		var timePerFrame = Time.FromSeconds(1.0f / 60.0f);
		var duration = Time.Zero;

		bool running = true;

		window.KeyPressed += (sender, ev) =>
		{
			if(ev.Code == Keyboard.Key.Escape)
			{
				running = false;
			}
		};

		var main = new MainClass();

		const string libraryFileName = "library.xml";
		main.library = new Library().LoadFromFile(libraryFileName);
		main.PopulateLibraryWithGameCollections();
		main.library.SaveToFile(libraryFileName);

		Theme.LightTheme = new Theme()
		{
			TextColor = new Color(0x2f2f2fff),
			BackgroundColor = new Color(0xf7f7f7ff),
			CursorOutlineColor = new Color(0x10a6c0ff),
            FontName = "OpenSans-Regular.ttf"
		};

		Theme.DarkTheme = new Theme()
		{
			TextColor = new Color(0xf7f7f7ff),
			BackgroundColor = new Color(0x2f2f2fff),
			CursorOutlineColor = new Color(0x10a6c0ff),
            FontName = "OpenSans-Regular.ttf"
		};

		Theme.SelectedTheme = Theme.LightTheme;

		main.currentView = new LoginView();
		main.currentView.Init(main, window);

		while (running)
		{
			window.DispatchEvents();

			Joystick.Update();

			if(main.pendingView != null)
			{
                main.currentView.Deinit();
				main.currentView = main.pendingView;
				main.currentView.Init(main, window);
				main.pendingView = null;
			}

			timeSinceLastUpdate += tickClock.Restart();

			while (timeSinceLastUpdate > timePerFrame)
			{
				timeSinceLastUpdate -= timePerFrame;

				main.currentView.Update(window, timePerFrame.AsMilliseconds());

				window.Clear(Theme.SelectedTheme.BackgroundColor);
				main.currentView.Render(window);
				window.Display();
			}
		}

		window.Close();
	}
}
