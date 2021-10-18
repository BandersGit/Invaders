using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders
{
    class Program
    {
        public const int ScreenW = 500;
        public const int ScreenH = 700;

        static void Main(string[] args)
        {
            using (var window = new RenderWindow(new VideoMode(ScreenW, ScreenH), "Invaders"))
            {
                window.Closed += (o, e) => window.Close();

                Clock clock = new Clock();
                Scene scene = new Scene();

                while(window.IsOpen)
                {
                    window.DispatchEvents();
                    float deltaTime = clock.Restart().AsSeconds();

                    scene.UpdateAll(deltaTime);

                    window.Clear();

                    scene.RenderAll(window);
                    window.Display();
                }
            }
        }
    }
}
