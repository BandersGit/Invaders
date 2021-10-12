using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Explosion : Entity
    {
        private float existTimer;

        public Explosion(Vector2f Position) : base("deathExplosion")
        {
            this.Position = Position;
        }

        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.Scale = new Vector2f (0.5f, 0.5f);
            sprite.Origin = new Vector2f(sprite.TextureRect.Width / 2, sprite.TextureRect.Height / 2);
            existTimer = 3.0f;
        }

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
            existTimer = MathF.Max(existTimer - deltaTime, 0.0f);

            if (existTimer <= 0.0f)
            {
                Dead = true;
            }
        }

        public override void Render(RenderTarget target)
        {
            sprite.Color = new Color(255, 255, 255 /*fadeOut*/);
            base.Render(target);
        }


    }
}
