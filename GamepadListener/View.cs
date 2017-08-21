using SFML.Graphics;
using SFML.Window;

namespace GamepadListener
{
	interface IView
	{
		void Init(MainClass main, RenderWindow window);
		void Update(Window window, int dt);
		void Render(RenderWindow window);
	}
}
