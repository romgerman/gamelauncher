using System;

using SFML.Graphics;
using SFML.Window;
using SFML.System;

using GamepadListener;
using GamepadListener.GameCollections;
using System.Collections.Generic;

class GameLauncher
{
	private IDrawable pendingView;
	private IDrawable currentView;

	private bool shutdownRequested;

	private RenderWindow window;
	public Session Session { get; set; }
	public Library Library { get; set; }
	public ThemeManager ThemeManager { get; set; }

	public static GameLauncher Instance { get; set; }

	public GameLauncher()
	{
		Instance = this;
	}

	// TODO: I hate this
	void PopulateLibraryWithGameCollections()
	{
		List<IGameCollection> gameCollections = new List<IGameCollection>
		{
			new SteamCollection(),
			new OriginCollection(),
			new UplayCollection()
		};

		foreach(var coll in gameCollections)
		{
			coll.FetchGameList();

			foreach(var g in coll.Games)
			{
				if(!Library.HasWithName(g.Name))
				{
					Library.AddItem(new LibraryItemApplication()
					{
						Name = g.Name,
						Path = g.Path
					});
				}
			}
		}
	}

	public void Initialize()
	{
		window = new RenderWindow(new VideoMode(1360, 768, 32), "launcher");
		window.Closed += (sender, ev) =>
		{
			shutdownRequested = true;
		};
		window.KeyPressed += (sender, ev) =>
		{
			var win = (Window)sender;
			if(win.HasFocus())
			{
				shutdownRequested = true;
			}
		};

		Session = new Session(0, "");

		Library = new Library().LoadFromFile(Paths.LibraryPath);
		PopulateLibraryWithGameCollections();
		Library.SaveToFile(Paths.LibraryPath);

		ThemeManager = new ThemeManager();
		ThemeManager.AddTheme("DarkTheme", new Theme()
		{
			TextColor = new Color(0xf7f7f7ff),
			BackgroundColor = new Color(0x2f2f2fff),
			CursorOutlineColor = new Color(0x10a6c0ff),
			FontName = "OpenSans-Regular.ttf"
		});
		ThemeManager.AddTheme("LightTheme", new Theme()
		{
			TextColor = new Color(0x2f2f2fff),
			BackgroundColor = new Color(0xf7f7f7ff),
			CursorOutlineColor = new Color(0x10a6c0ff),
			FontName = "OpenSans-Regular.ttf"
		});
		ThemeManager.SetTheme("LightTheme");
	}

	public void Run(IDrawable initialView)
	{
		Initialize();

		ChangeView(initialView);

		var tickClock = new Clock();
		var timeSinceLastUpdate = Time.Zero;
		var timePerFrame = Time.FromSeconds(1.0f / 60.0f);

		while (!shutdownRequested)
		{
			ProcessPendingView();

			window.DispatchEvents();
			Joystick.Update();

			timeSinceLastUpdate += tickClock.Restart();

			while (timeSinceLastUpdate > timePerFrame)
			{
				timeSinceLastUpdate -= timePerFrame;

				currentView.Update(window, timePerFrame.AsMilliseconds());

				window.Clear(ThemeManager.SelectedTheme.BackgroundColor);
				currentView.Render(window);
				window.Display();
			}
		}

		currentView.Deinit();
		window.Close();
	}

	public void Quit()
	{
		shutdownRequested = true;
	}

	public void ChangeView(IDrawable newView)
	{
		if(currentView != null)
		{
			pendingView = newView;
		}
		else
		{
			SwitchView(newView);
		}
	}

	private void ProcessPendingView()
	{
		if(HasPendingView())
		{
			SwitchView(pendingView);
			pendingView = null;
		}
	}

	private void SwitchView(IDrawable newView)
	{
		if(currentView != null)
		{
			currentView.Deinit();
		}
		currentView = newView;
		currentView.Init(this, window);
	}

	private bool HasPendingView()
	{
		return pendingView != null;
	}
}


class MainClass
{
	static void Main(string[] args)
	{
		GameLauncher launcher = new GameLauncher();
		launcher.Run(new LoginView());
	}
}