using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Entity
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

        public virtual bool Solid => false;

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

        public virtual void Update(Scene scene, float deltaTime)
        {

        }

        public virtual void Render(RenderTarget target)
        {
            target.Draw(sprite);
        }

    }
}
