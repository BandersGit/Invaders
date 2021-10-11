using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Entity
    {
        private string textureName;
        protected Sprite sprite;
        public bool Dead;

        protected Entity(string textureName)
        {
            this.textureName = textureName;
            sprite = new Sprite();
        }

        public Vector2f Position
        {
            get => sprite.Position;
            set => sprite.Position = value;
        }

        public virtual void Create(Scene scene)
        {
            sprite.Texture = scene.Assets.LoadTexture(textureName);
        }

    }
}
