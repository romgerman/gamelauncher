using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GamepadListener.UI.Layouts
{
	// TODO: can be horizontal and vertical

	/// <summary>
	/// Stackable container layout for UI objects
	/// </summary>
	class UIStack : IUIElement
	{
		public Vector2f Offset
		{
			get { return _offset; }
			set { _offset = value; }
		}

		private Vector2f _offset;

		public FloatRect Bounds => new FloatRect();

		public IUIElement Parent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void Init(GameLauncher launcher, RenderWindow window)
		{
			throw new NotImplementedException();
		}

		public void Deinit()
		{
			throw new NotImplementedException();
		}

		public void Render(RenderWindow window)
		{
			throw new NotImplementedException();
		}

		public void Update(Window window, int dt)
		{
			throw new NotImplementedException();
		}
	}
}
