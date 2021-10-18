using System;

namespace Invaders
{
    public class SceneManager
    {
        public void HandleSceneReload(Scene scene)
        {
            if (scene.Reload)
            {
                scene.Clear();
                scene.Spawn(new Background());
                scene.Spawn(new Player());
                scene.Spawn(new GUI());
                scene.Spawn(new ShipSpawner());
                scene.Reload = false;
            }
        }
    }
}
