using System;

using SFML.System;
using SFML.Graphics;
using SFML.Window;

using GamepadListener.UI.Layouts;

namespace GamepadListener.UI
{
	/// <summary>
	/// Text object
	/// </summary>
	class UIText : IUIElement
	{
		private Text _text;

		public string Text
		{
			get { return _text.DisplayedString; }
			set { _text.DisplayedString = value; }
		}

		public uint FontSize
		{
			get { return _text.CharacterSize; }
			set { _text.CharacterSize = value; }
		}

		public Color ForegroundColor
		{
			get { return _text.FillColor; }
			set { _text.FillColor = value; }
		}

		public Vector2f Offset
		{
			get { return _offset; }
			set
			{
				_offset = value;
				_text.Position = _offset;
			}
		}

		private Vector2f _offset;

		public FloatRect Bounds => _text.GetLocalBounds();

		public IUIElement Parent
		{
			get { return _parent; }
			set { _parent = value; }
		}

		private IUIElement _parent;

		public UIAlignmentHorizontal HorizontalAlingment
		{
			get { return _horizAlignment; }
			set
			{
				_horizAlignment = value;

				switch (_horizAlignment)
				{
					case UIAlignmentHorizontal.Left:
						{
							var ppos = _parent == null ? 0.0f : _parent.Offset.X;

							this.Offset = new Vector2f(ppos, this.Offset.Y);
						}
						break;
					case UIAlignmentHorizontal.Center:
						{
							var ppos = _parent == null ? 0.0f : _parent.Offset.X;
							var bpos = _parent == null ? 0.0f : _parent.Bounds.Width;
							ppos += bpos / 2 - this.Bounds.Width / 2;

							this.Offset = new Vector2f(ppos, this.Offset.Y);
						}
						break;
					case UIAlignmentHorizontal.Right:
						{
							var ppos = _parent == null ? 0.0f : _parent.Offset.X;
							var bpos = _parent == null ? 0.0f : _parent.Bounds.Width;
							ppos += bpos - this.Bounds.Width;

							this.Offset = new Vector2f(ppos, this.Offset.Y);
						}
						break;
				}
			}
		}

		private UIAlignmentHorizontal _horizAlignment;

		public UIAlignmentVertical VerticalAlingment
		{
			get { return _vertAlignment; }
			set
			{
				_vertAlignment = value;

				switch (_vertAlignment)
				{
					case UIAlignmentVertical.Top:
						{
							var ppos = _parent == null ? 0.0f : _parent.Offset.Y;

							this.Offset = new Vector2f(this.Offset.X, ppos);
						}
						break;
					case UIAlignmentVertical.Middle:
						{
							var ppos = _parent == null ? 0.0f : _parent.Offset.Y;
							var bpos = _parent == null ? 0.0f : _parent.Bounds.Height;
							ppos += bpos / 2 - this.Bounds.Height / 2;

							this.Offset = new Vector2f(this.Offset.X, ppos);
						}
						break;
					case UIAlignmentVertical.Bottom:
						{
							var ppos = _parent == null ? 0.0f : _parent.Offset.Y;
							var bpos = _parent == null ? 0.0f : _parent.Bounds.Height;
							ppos += bpos - this.Bounds.Height;

							this.Offset = new Vector2f(ppos, this.Offset.Y);
						}
						break;
				}
			}
		}

		private UIAlignmentVertical _vertAlignment;

		public UIText()
		{
			_text = new Text("", GameLauncher.Instance.ThemeManager.SelectedTheme.GetFont());
		}

		public UIText(string text)
		{
			_text = new Text(text, GameLauncher.Instance.ThemeManager.SelectedTheme.GetFont());
		}

		#region IDrawable interfaces

		public void Init(GameLauncher launcher, RenderWindow window)
		{
			
		}

		public void Deinit()
		{
			_text.Dispose();
		}

		public void Render(RenderWindow window)
		{
			_text.Draw(window, RenderStates.Default);
		}

		public void Update(Window window, int dt)
		{
			
		}

		#endregion
	}

}
