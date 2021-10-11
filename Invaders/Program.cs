using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var window = new RenderWindow(new VideoMode(500, 700), "Invaders"))
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
