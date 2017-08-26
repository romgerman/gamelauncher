using SFML.Graphics;
using SFML.Window;

namespace GamepadListener
{
	interface IDrawable
	{
		void Init(GameLauncher launcher, RenderWindow window);
        void Deinit();
		void Update(Window window, int dt);
		void Render(RenderWindow window);
	}
}
