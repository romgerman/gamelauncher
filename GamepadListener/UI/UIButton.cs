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

		private float _padding;

		public IUIElement Body
		{
			get { return _body; }
			set { _body = value; }
		}

		private IUIElement _body;

		public Vector2f Offset
		{
			get { return _offset; }
			set { _offset = value; }
		}

		private Vector2f _offset;

		public FloatRect Bounds => _background.GetLocalBounds();

		public IUIElement Parent
		{
			get { return _parent; }
			protected set { _parent = value; }
		}

		private IUIElement _parent;

		public UIButton()
		{
			_background = new RectangleShape();
		}

		public T GetBody<T>() where T : IUIElement
		{
			return (T)_body;
		}

		public void Init(MainClass main, RenderWindow window)
		{
			_body.Init(main, window);
		}

		public void Deinit()
		{
			_background.Dispose();
		}

		public void Render(RenderWindow window)
		{
			_background.Draw(window, RenderStates.Default);
		}

		public void Update(Window window, int dt)
		{
			_body.Update(window, dt);
		}
	}
}
