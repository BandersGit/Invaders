using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Bullet : Entity
    {
        private Vector2f direction;
        private float bulletSpeed = 500.0f;
        public readonly Entity ShotOrigin;

        public Bullet(Vector2f direction, Entity ShotOrigin) : base("spaceSheet")
        {
             this.direction = direction;
             this.ShotOrigin = ShotOrigin;
        }

        protected override bool Solid => true;

        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = new IntRect(835, 565, 13, 37);
            sprite.Origin = new Vector2f(sprite.TextureRect.Width/2 , sprite.TextureRect.Height/2);
            sprite.Rotation = ((180 / MathF.PI) * MathF.Atan2(direction.Y, direction.X)) + 90;
        }

        public override void Update(Scene scene, float deltaTime)
        {
            
            Position += direction * bulletSpeed * deltaTime;

            if (Position.X < 0 - sprite.Origin.X 
                ||Position.X > Program.ScreenW + sprite.Origin.X 
                ||Position.Y < 0 - sprite.Origin.Y 
                ||Position.Y > Program.ScreenH + sprite.Origin.Y)
            {
                Dead = true;
            }
        }

        public override void Render(RenderTarget target)
        {
            base.Render(target);
        }
    }
}
