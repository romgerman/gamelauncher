using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GamepadListener
{
	class MenuItem
	{
		public string Name;
		public Color Color;
		public RectangleShape Rectangle;
		public Text Title;
		//public Sprite Cover;
	}

	class MenuContainer : IView
	{
		private List<MenuItem> elements;
		private Vector2f menuPosition;
		private RenderWindow rwindow;

		private int selectedIndex;

		public MenuContainer()
		{
			this.elements = new List<MenuItem>();
		}

		public void SetPosition(Vector2f pos)
		{
			this.menuPosition = pos;
		}

		public void AddItem(string name, Color color)
		{
			float elementRenderingSize = (rwindow.Size.X - 50 - 20 - 20 - 20 - 20) / 4.0f;

			float yPos = 150f;
			yPos = rwindow.Size.Y / 4.0f;

			var item = new MenuItem()
			{
				Name = name,
				Color = color,
				Rectangle = new RectangleShape(new Vector2f(elementRenderingSize, elementRenderingSize))
				{
					FillColor = color,
					OutlineThickness = 10.0f
				}
			};

			item.Rectangle.Position = new Vector2f(item.Rectangle.GetGlobalBounds().Width * elements.Count + elements.Count * 20 + 20, yPos);

			//if (item.Cover != null)
			//{
				//item.Cover.Scale = new Vector2f(elementRenderingSize / (float)item.Cover.Texture.Size.X, elementRenderingSize / (float)item.Cover.Texture.Size.Y);
			//}

			elements.Add(item);
		}

		public void SelectItem(int index)
		{
			elements[selectedIndex].Rectangle.OutlineColor = Color.Transparent;
			elements[index].Rectangle.OutlineColor = Color.Cyan;

			float itemOffset = elements[0].Rectangle.GetGlobalBounds().Width * index + index * 20 + 20;

			// Very dirty

			if (itemOffset > rwindow.Size.X)
			{
				for (int i = 0; i < elements.Count; i++)
				{
					float addOffset = elements[0].Rectangle.GetGlobalBounds().Width + 20;
					elements[i].Rectangle.Position = new Vector2f(elements[i].Rectangle.Position.X - addOffset,
																  elements[i].Rectangle.Position.Y);
				}
			}
			else if (elements[index].Rectangle.Position.X + 20 < 0)
			{
				for (int i = 0; i < elements.Count; i++)
				{
					float addOffset = (elements[0].Rectangle.GetGlobalBounds().Width + 20);
					elements[i].Rectangle.Position = new Vector2f(elements[i].Rectangle.Position.X + addOffset,
																  elements[i].Rectangle.Position.Y);
				}
			}

			this.selectedIndex = index;
		}
		
		public void SelectNext()
		{
			if (selectedIndex + 1 == elements.Count)
				SelectItem(0);
			else
				SelectItem(selectedIndex + 1);
		}
		
		public void SelectPrev()
		{
			if (selectedIndex - 1 < 0)
				SelectItem(elements.Count - 1);
			else
				SelectItem(selectedIndex - 1);
		}

		public MenuItem GetSelectedItem()
		{
			return elements[selectedIndex];
		}

		public bool Empty()
		{
			return elements.Count == 0;
		}

		public void Init(MainClass main, RenderWindow window)
		{
			this.rwindow = window;

			// This is just for testing

			SteamCollection collection = new SteamCollection();
			collection.FetchGameList();

			foreach(string name in collection.games)
			{
				AddItem(name, Color.Black);
			}

			AddItem("First", Color.Green);
			AddItem("Game Number Two", Color.Red);
			AddItem("Game Number Three", Color.Black);
			AddItem("Game Number Four", Color.Yellow);
			AddItem( "Game Number Five", Color.Magenta);

			SelectItem(0);
		}

		public void Render(RenderWindow window)
		{
			for (int i = 0; i < elements.Count; i++)
			{
				elements[i].Rectangle.Draw(window, RenderStates.Default);
			}
		}

		public void Update(Window window, int dt)
		{
			// ...
		}
	}
}
