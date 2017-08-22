﻿using System;
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
		public bool IsVisible;
		public TweenVector2f Animation;
		//public Sprite Cover;

		public void AnimatePosition(Vector2f newpos, int duration)
		{
			Animation.Animate(Rectangle.Position, newpos, duration, TweenType.Linear);
		}

		public void UpdateAnimation(int dt)
		{
			var pos = Rectangle.Position;
			Animation.Update(ref pos, dt);
			Rectangle.Position = pos;
			Title.Position = new Vector2f(Rectangle.Position.X, Title.Position.Y);
		}
	}

	class MenuContainer : IView
	{
		private List<MenuItem> elements;
		private Vector2f menuPosition;
		private RenderWindow rwindow;

		private int offset = 20;
		private float elementSizeWithOffset = 0;
		private int maxWholeElementsOnScreen = 0;

		private int selectedIndex = 0;

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
			elementSizeWithOffset = elementRenderingSize + offset * 2 + 20; // 20 is outline
			maxWholeElementsOnScreen = (int)(rwindow.Size.X / elementSizeWithOffset);

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
				},
				Title = new Text(name, Theme.SelectedTheme.GetFont(), 16),
				Animation = new TweenVector2f() 
			};

			item.Rectangle.Position = new Vector2f(elementSizeWithOffset * elements.Count + offset * 2, yPos);
			item.IsVisible = true; // TODO: visibility (item.Rectangle.Position.X <= rwindow.Size.X;)

			item.Title.FillColor = Color.Black;
			item.Title.Position = new Vector2f(item.Rectangle.Position.X, item.Rectangle.Position.Y + item.Rectangle.GetLocalBounds().Height + item.Title.GetLocalBounds().Height);

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

			if (selectedIndex < index)
			{
				for (int i = 0; i < elements.Count; i++)
				{
					var position = elements[i].Rectangle.Position.X;

					if (elements[i].Animation.IsRunning())
					{
						position = elements[i].Animation.End().X;
					}

					elements[i].AnimatePosition(new Vector2f(position - elementSizeWithOffset,
															 elements[i].Rectangle.Position.Y), 500);
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

		private void ScrollLeft(float dt)
		{
			for (int i = 0; i < elements.Count; i++)
			{
				elements[i].Rectangle.Position = Lerp(elements[i].Rectangle.Position,
														new Vector2f(elements[i].Rectangle.Position.X - elementSizeWithOffset, elements[i].Rectangle.Position.Y),
														dt);
				elements[i].Title.Position = Lerp(elements[i].Title.Position,
														new Vector2f(elements[i].Title.Position.X - elementSizeWithOffset, elements[i].Title.Position.Y),
														dt);
			}
		}

		private void ScrollRight(float dt)
		{
			for (int i = 0; i < elements.Count; i++)
			{
				elements[i].Rectangle.Position = Lerp(elements[i].Rectangle.Position,
														new Vector2f(elements[i].Rectangle.Position.X + elementSizeWithOffset, elements[i].Rectangle.Position.Y),
														dt);
				elements[i].Title.Position = Lerp(elements[i].Title.Position,
														new Vector2f(elements[i].Title.Position.X + elementSizeWithOffset, elements[i].Title.Position.Y),
														dt);
			}
		}

		private Vector2f Lerp(Vector2f v1, Vector2f v2, float t)
		{
			return v1 + (v2 - v1) * t;
		}

		public void Init(MainClass main, RenderWindow window)
		{
			this.rwindow = window;

			// This is just for testing

			SteamCollection collection = new SteamCollection();
			//collection.FetchGameList();

			//foreach(string name in collection.games)
			//{
				//AddItem(name, Color.Black);
			//}

			AddItem("First game", Color.Green);
			AddItem("Game Number Two", Color.Red);
			AddItem("Game Number Three", Color.Black);
			AddItem("Game Number Four", Color.Yellow);
			AddItem("Game Number Five", Color.Magenta);
			AddItem("Game Number Six", Color.Blue);
			AddItem("Game Number Seven", Color.Red);
			AddItem("Game Number Eight", Color.Cyan);
			AddItem("Game Number Nine", Color.Black);

			SelectItem(0);
		}

		public void Render(RenderWindow window)
		{
			for (int i = 0; i < elements.Count; i++)
			{
				if (elements[i].IsVisible)
				{
					elements[i].Rectangle.Draw(window, RenderStates.Default);
					elements[i].Title.Draw(window, RenderStates.Default);
				}
			}
		}

		public void Update(Window window, int dt)
		{
			for (int i = 0; i < elements.Count; i++)
			{
				elements[i].UpdateAnimation(dt);
			}
		}
	}
}
