using System;

using SFML.System;
using SFML.Graphics;

namespace GamepadListener.UI
{
	/// <summary>
	/// Main interface for all UI elements
	/// </summary>
	interface IUIElement : IDrawable
	{
		Vector2f Offset { get; set; }
		FloatRect Bounds { get; }
		IUIElement Parent { get; }
	}
}
