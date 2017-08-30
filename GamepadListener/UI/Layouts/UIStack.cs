using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GamepadListener.UI.Layouts
{
	enum UIStackOrientation
	{
		Vertical, Horizontal
	}

	/// <summary>
	/// Stackable container layout for UI objects
	/// </summary>
	class UIStack : IUIElement, IEnumerable<IUIElement>
	{
		private List<IUIElement> _elements;
		private UIStackOrientation _orientation;

		public Vector2f Offset
		{
			get { return _offset; }
			set { _offset = value; }
		}

		private Vector2f _offset;

		public FloatRect Bounds => new FloatRect();

		public IUIElement Parent
		{
			get { return _parent; }
			set { _parent = value; }
		}

		private IUIElement _parent;

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

		public UIStack(UIStackOrientation orientation = UIStackOrientation.Vertical)
		{
			this._elements = new List<IUIElement>();
			this._orientation = orientation;
		}

		public void Add(IUIElement item)
		{
			_elements.Add(item);
		}

		public void Remove(IUIElement item)
		{
			_elements.Remove(item);
		}

		#region IDrawable interface

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

		#endregion

		#region IEnumerable interface

		public IEnumerator<IUIElement> GetEnumerator()
		{
			return _elements.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _elements.GetEnumerator();
		}

		#endregion
	}
}
