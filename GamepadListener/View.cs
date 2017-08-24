using SFML.Graphics;
using SFML.Window;

namespace GamepadListener
{
	interface View
	{
		void Init(MainClass main, RenderWindow window);
        void Deinit();
		void Update(Window window, int dt);
		void Render(RenderWindow window);
	}
}
