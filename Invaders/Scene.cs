using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace Invaders
{
    public class Scene
    {
        private List<Entity> entities;
        public readonly AssetManager Assets;
        public readonly SceneManager Reloader;
        public readonly EventManager Events;
        public bool Reload = false;

        public Scene()
        {
            entities = new List<Entity>();
            Assets = new AssetManager();
            Events = new EventManager();
            Reloader = new SceneManager();

            Spawn(new Background());
            Spawn(new Player());
            Spawn(new GUI());
            Spawn(new ShipSpawner());
        }

        public void Spawn(Entity entity)
        {
            entities.Add(entity);
            entity.Create(this);
        }

        public void Clear()
        {
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                Entity entity = entities[i];
                entities.RemoveAt(i);
                entity.Destroy(this);
            }
        }

        public IEnumerable<Entity> FindInstersects(FloatRect bounds)
        {
            int lastEntity = entities.Count - 1;

            for (int i = lastEntity; i >= 0; i--)
            {
                Entity entity = entities[i];

                if (entity.Dead) continue;

                if (entity.Bounds.Intersects(bounds))
                {
                    yield return entity;
                }
            }
        }

        public void UpdateAll(float deltaTime)
        {
            Reloader.HandleSceneReload(this);

            for (int i = entities.Count - 1; i >= 0; i--)
            {
                Entity entity = entities[i];
                entity.Update(this, deltaTime);
            }

            Events.HandleEvents(this);

            for (int i = 0; i < entities.Count;)
            {
                Entity entity = entities[i];

                if (entity.Dead)
                {
                    entities.RemoveAt(i);
                    entity.Destroy(this);
                }else
                i++;
            }
        }

        public void RenderAll(RenderTarget target)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                Entity entity = entities[i];
                entity.Render(target);
            }
        }
    }
}
