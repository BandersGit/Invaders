using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Explosion : Entity
    {
        private float existTimer;
        public Explosion() : base("deathExplosion")
        {
            
        }

        public override void Create(Scene scene) //Find a way to get the position of the dead ship
        {
            base.Create(scene);
            existTimer = 3.0f;
        }

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
            existTimer = MathF.Max(existTimer - deltaTime, 0.0f);

            if (existTimer > 0.0f)
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
