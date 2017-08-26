using System;

using SFML.System;
using SFML.Graphics;

using GamepadListener.UI.Layouts;

namespace GamepadListener.UI
{
	// TODO: Frankly idk about this interface. Maybe we should make it as an abstract class
	// Because i hate making all properties and fileds for them :/

	// TODO: Also we need to make UI input system somehow
	// So all interactable elements like buttons and input boxes should be registered in it

	/// <summary>
	/// Main interface for all UI elements
	/// </summary>
	interface IUIElement : IDrawable
	{
		Vector2f Offset { get; set; }
		FloatRect Bounds { get; }
		IUIElement Parent { get; }
		UIAlignmentHorizontal HorizontalAlingment { get; set; }
		UIAlignmentVertical VerticalAlingment { get; set; }
	}
}
