using System.Text;
using System.Collections.Generic;
using System;
using SFML.System;
using SFML.Graphics;
using System.IO;

namespace Invaders
{
    public class Scene
    {
        private List<Entity> entities;
        public readonly AssetManager Assets;
        public readonly EventManager Events;

        public Scene()
        {
            entities = new List<Entity>();
            Assets = new AssetManager();
            Events = new EventManager();
        }

        public void Spawn(Entity entity)
        {
            entities.Add(entity);
            entity.Create(this);
        }

        public bool TryMove(Entity entity, Vector2f movement)   //Checks collisions and limits movements based on those
        {
            entity.Position += movement;
            bool collided = false;
            
            for (int i = 0; i < entities.Count; i++)
            {
                Entity other = entities[i];
                if (!other.Solid) continue;
                if (other == entity) continue;

                FloatRect boundsA = entity.Bounds;
                FloatRect boundsB = other.Bounds;
                if (Collision.RectangleRectangle(boundsA, boundsB, out Collision.Hit hit))
                {
                    entity.Position += hit.Normal * hit.Overlap;
                    i = -1;
                    collided = true;
                }
            }

            return collided;
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

        public bool FindByType<T>(out T found) where T : Entity
        {
            foreach (Entity entity in entities)
            {
                if (entity is T typed && !entity.Dead)
                {
                    found = typed;
                    return true;
                }
            }

            found = default(T);
            return false;
        }

        public void UpdateAll(float deltaTime)
        {
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
