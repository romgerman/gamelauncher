using System;

using SFML.System;
using SFML.Graphics;

using GamepadListener.UI.Layouts;

namespace GamepadListener.UI
{
	// TODO: Also we need to make UI input system somehow
	// So all interactable elements like buttons and input boxes should be registered in it

	/// <summary>
	/// Main interface for all UI elements
	/// </summary>
	interface IUIElement : IDrawable
	{
		Vector2f Offset { get; set; } // TODO: i thought this could be used as padding/margin but it used to position elements. So either we make this to work with both or just add padding/margin properties
		FloatRect Bounds { get; }
		IUIElement Parent { get; set; }
		UIAlignmentHorizontal HorizontalAlingment { get; set; }
		UIAlignmentVertical VerticalAlingment { get; set; }
	}
}
