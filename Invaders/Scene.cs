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

        public Scene()
        {
            entities = new List<Entity>();
            Assets = new AssetManager();
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

        public void UpdateAll(float deltaTime)
        {

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
