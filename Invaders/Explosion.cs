using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Explosion : Entity
    {
        private float existTimer;
        private float fadeOut;

        public Explosion(Vector2f Position) : base("deathExplosion")
        {
            this.Position = Position;
        }

        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.Scale = new Vector2f (0.5f, 0.5f);
            sprite.Origin = new Vector2f(sprite.TextureRect.Width / 2, sprite.TextureRect.Height / 2);
            existTimer = 0.25f;
            fadeOut = existTimer;
        }

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
            existTimer = MathF.Max(existTimer - deltaTime, 0.0f);

            if (existTimer <= 0.0f)
            {
                Dead = true;
            }

            if (fadeOut< deltaTime)
            {
                fadeOut= 0.0f;
            }else
            {
                fadeOut -= deltaTime;
                byte a = (byte) (fadeOut/ 0.25f * 255.0f);
                sprite.Color = new Color(255, 255, 255, a);
            }
           
        }

        public override void Render(RenderTarget target)
        {
            
            base.Render(target);
        }


    }
}
