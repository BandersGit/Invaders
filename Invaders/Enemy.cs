using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Enemy : Entity
    {
        private Vector2f direction = new Vector2f(1, 1) / MathF.Sqrt(2.0f);
        private const float shipSpeed = 300.0f;
        private Vector2f originalPosition;

        public Enemy() : base("spaceSheet")
        {

        }

        protected override bool Solid => true;

        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = new IntRect(346, 150, 97, 84);
            sprite.Origin = new Vector2f(sprite.TextureRect.Width/2 , sprite.TextureRect.Height/2);
            sprite.Scale = new Vector2f (0.7f, 0.7f);
            sprite.Rotation = ((180 / MathF.PI) * MathF.Atan2(direction.Y, direction.X)) + -90;
            originalPosition = Position;
        }

        public override void Destroy(Scene scene)
        {
            scene.Spawn(new Explosion(Position));
        }

        protected override void CollideWith(Scene scene, Entity other)
        {
            if (other is Bullet bullet)
            {
                if (bullet.ShotOrigin is Player)
                {
                    other.Dead = true;
                    Dead = true;
                }
            }
        }

        public override void Update(Scene scene, float deltaTime)
        {
            var newPos = Position;
            newPos += direction * deltaTime * shipSpeed;
            
            if (newPos.X > Program.ScreenW - sprite.Origin.X)
            {
                newPos.X = Program.ScreenW - sprite.Origin.X;
                Reflect(new Vector2f(-1, 0));
            }
            else if (newPos.X < 0 + sprite.Origin.X)
            {
                newPos.X = 0 + sprite.Origin.X;
                Reflect(new Vector2f(1, 0));
            }
            else if (newPos.Y < 0 + sprite.Origin.X)
            {
                newPos.Y = 0 + sprite.Origin.X;
                Reflect(new Vector2f(0, 1));
            }

            if (newPos.Y > Program.ScreenH + sprite.Origin.X) //Moves the enemy up if it exits the bottom
            {
                Position = originalPosition;

            }else
            {
                Position = newPos;
            }
            
            base.Update(scene, deltaTime);
        }

        private void Reflect(Vector2f normal)
        {
            direction -= normal * (2 * (direction.X * normal.X + direction.Y * normal.Y));
            sprite.Rotation = ((180 / MathF.PI) * MathF.Atan2(direction.Y, direction.X)) + -90;
        }

        public override void Render(RenderTarget target)
        {
            target.Draw(sprite);
        }
    }
}
