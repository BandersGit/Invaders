using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Invaders
{
    public class Player : Entity
    {
        private const float shipSpeed = 500.0f;
        protected float invincTimer;

        public Player() : base("spaceSheet")
        {
            
        }

        public override bool Solid => true;

        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = new IntRect(325, 0, 98, 75);
            sprite.Origin = new Vector2f(sprite.TextureRect.Width / 2, sprite.TextureRect.Height / 2);
            Position = new Vector2f(125, 350);
            size = new Vector2f(sprite.GetGlobalBounds().Width, sprite.GetGlobalBounds().Height);
            scene.Events.LoseHealth += OnLoseHealth;
        }

        private void OnLoseHealth(Scene scene, int amount)
        {
            //Loss of health gets handled in GUI
            
            System.Console.WriteLine("HIT");
            invincTimer = 3.0f;
        }

        protected override void CollideWith(Scene scene, Entity other)
        {
            if(other is Enemy)
            {
                if (invincTimer <= 0.0f)
                {
                    scene.Events.PublishLoseHealth(1);
                }

                invincTimer = 0.0f;
            }
        }

        public override void Update(Scene scene, float deltaTime)
        {
            var newPos = Position;
            //Side borders
            if (Position.X > Program.ScreenW - sprite.Origin.X) //Removed elses to fix border collision bugs
            {
                newPos.X = Program.ScreenW - sprite.Origin.X;
            }
            if (newPos.X < 0 + sprite.Origin.X)
            {
                newPos.X = 0 + sprite.Origin.X;
            }

            //Upper and lower borders
            if (newPos.Y > Program.ScreenH - sprite.Origin.Y)
            {
                newPos.Y = Program.ScreenH - sprite.Origin.Y;
            }
            if (newPos.Y < 0 + sprite.Origin.Y)
            {
                newPos.Y = 0 + sprite.Origin.Y;
            }

            Position = newPos;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                Position += new Vector2f(1, 0) * shipSpeed * deltaTime;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                Position += new Vector2f(-1, 0) * shipSpeed * deltaTime;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                Position += new Vector2f(0, -1) * shipSpeed * deltaTime;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                Position += new Vector2f(0, 1) * shipSpeed * deltaTime;
            }


            base.Update(scene, deltaTime);
            invincTimer = MathF.Max(invincTimer - deltaTime, 0.0f);
        }

        public override void Render(RenderTarget target)
        {
            if (invincTimer > 0.0f)
            {
                System.Console.WriteLine(invincTimer);
            }
            base.Render(target);
        }
    }
}
