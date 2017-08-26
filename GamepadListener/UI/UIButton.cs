using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace GamepadListener.UI
{
	/// <summary>
	/// Button. Clickable container for UI objects
	/// </summary>
	class UIButton : IUIElement
	{
		private RectangleShape _background;
		
		public Color Background
		{
			get { return _background.FillColor; }
			set { _background.FillColor = value; }
		}

		public float Padding
		{
			get { return _padding; }
			set { _padding = value; }
		}

		private float _padding = 5f;

		public IUIElement Body
		{
			get { return _body; }
			set
			{
				_body = value;
				_body.Offset = new Vector2f(_offset.X + _padding, _offset.Y + _padding);
				_background.Size = new Vector2f(_body.Bounds.Width + _padding * 2,
												_body.Bounds.Height + _padding * 2);
			}
		}

		private IUIElement _body;

		public Vector2f Offset
		{
			get { return _offset; }
			set
			{
				_offset = value;
				_background.Position = new Vector2f(value.X + _padding, value.Y + _padding);
			}
		}

		private Vector2f _offset;

		public FloatRect Bounds
		{
			get
			{
				var local = _background.GetLocalBounds();
				return new FloatRect(local.Left + _padding,
									local.Top + _padding,
									local.Width + _padding,
									local.Height + _padding);
			}
		}

		public IUIElement Parent
		{
			get { return _parent; }
			protected set { _parent = value; }
		}

		private IUIElement _parent;

		public UIButton()
		{
			_background = new RectangleShape();
			_background.FillColor = Color.Black;
		}

		public T GetBody<T>() where T : IUIElement
		{
			return (T)_body;
		}

		public void Init(GameLauncher launcher, RenderWindow window)
		{
			_body.Init(launcher, window);
		}

		public void Deinit()
		{
			_background.Dispose();
		}

		public void Render(RenderWindow window)
		{
			_background.Draw(window, RenderStates.Default);
			_body.Render(window);
		}

		public void Update(Window window, int dt)
		{
			_body.Update(window, dt);
		}
	}
}
