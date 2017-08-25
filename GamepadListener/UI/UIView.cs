using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GamepadListener.UI
{
	/// <summary>
	/// Container for UI objects. Stores and draws them
	/// </summary>
	class UIView : IUIElement, IEnumerable<IUIElement>
	{
		private HashSet<IUIElement> _elements;

		public int Count => _elements.Count;

		public FloatRect Bounds
		{
			get { return _size; }
			set { _size = value; }
		}

		private FloatRect _size;

		public Vector2f Offset
		{
			get { return _offset; }
			set { _offset = value; }
		}

		private Vector2f _offset;

		public IUIElement Parent
		{
			get { return _parent; }
			protected set { _parent = value; }
		}

		private IUIElement _parent;

		public UIView()
		{
			_elements = new HashSet<IUIElement>();
		}

		public void Add(IUIElement item)
		{
			_elements.Add(item);
		}

		public void Remove(IUIElement item)
		{
			_elements.Remove(item);
		}

		public void Clear()
		{
			_elements.Clear();
		}

		public void Init(MainClass main, RenderWindow window)
		{
			foreach(var i in _elements)
				i.Init(main, window);
		}

		public void Deinit()
		{
			foreach (var i in _elements)
				i.Deinit();
		}

		public void Render(RenderWindow window)
		{
			foreach (var i in _elements)
				i.Render(window);
		}

		public void Update(Window window, int dt)
		{
			foreach (var i in _elements)
				i.Update(window, dt);
		}

		public IEnumerator<IUIElement> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
