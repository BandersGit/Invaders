using System;
using SFML.Graphics;
using SFML.System;
using System.Linq;

namespace Invaders
{
    public abstract class Entity
    {
        private string textureName;
        protected Sprite sprite;
        protected Vector2f size;
        public bool Dead;

        protected Entity(string textureName)
        {
            this.textureName = textureName;
            sprite = new Sprite();
        }

        protected virtual bool Solid => false;

        public virtual FloatRect Bounds => sprite.GetGlobalBounds();

        public Vector2f Position
        {
            get => sprite.Position;
            set => sprite.Position = value;
        }

        public virtual void Create(Scene scene)
        {
            sprite.Texture = scene.Assets.LoadTexture(textureName);
        }

        public virtual void Destroy(Scene scene)
        {
            
        }

        protected virtual void CollideWith(Scene scene, Entity other)
        {

        }

        public virtual void Update(Scene scene, float deltaTime)
        {
            foreach(Entity found in scene.FindInstersects(Bounds).Where(e => e.Solid))
            {
                CollideWith(scene, found);
            }
        }

        public virtual void Render(RenderTarget target)
        {
            target.Draw(sprite);
        }

    }
}
