using SFML.Graphics;
using SFML.Window;

namespace GamepadListener
{
	interface View
	{
		void init(MainClass main, RenderWindow window);
		void update(Window window, int dt);
		void render(RenderWindow window);
	}
}
