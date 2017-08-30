using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;
using SFML.Window;

using GamepadListener.UI.Layouts;

namespace GamepadListener.UI
{
	/// <summary>
	/// Dialog object. Can be used for any dialogs
	/// </summary>
	class UIDialog : IUIElement
	{
		public Vector2f Offset
		{
			get { return _offset; }
			set { _offset = value; }
		}

		private Vector2f _offset;

		public FloatRect Bounds
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IUIElement Parent
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new Exception();
			}
		}

		public UIAlignmentHorizontal HorizontalAlingment
		{
			get { return _horizAlignment; }
			set { _horizAlignment = value; }
		}

		private UIAlignmentHorizontal _horizAlignment;

		public UIAlignmentVertical VerticalAlingment
		{
			get { return _vertAlignment; }
			set { _vertAlignment = value; }
		}

		private UIAlignmentVertical _vertAlignment;

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
