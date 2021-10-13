using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Invaders
{
    public class Player : Entity
    {
        private const float shipSpeed = 500.0f;
        private float invincTimer;
        private float fireRate = 0.2f;
        private float fireRateTimer = 0;
        
        

        public Player() : base("spaceSheet")
        {
            
        }

        protected override bool Solid => true;

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
            invincTimer = 3.0f;
        }

        protected override void CollideWith(Scene scene, Entity other)
        {
            if(other is Bullet bullet)
            {
                if (bullet.ShotOrigin != this)
                {
                    other.Dead = true;
                    if (invincTimer <= 0.0f)
                    {
                        scene.Events.PublishLoseHealth(1);
                    }
                }

            }else if (other is Enemy)
            {
                other.Dead = true;
                
                if (invincTimer <= 0.0f)
                {
                    scene.Events.PublishLoseHealth(1);
                }
            }
        }

        public override void Update(Scene scene, float deltaTime)
        {
            var newPos = Position;

            fireRateTimer += deltaTime;

            invincTimer = MathF.Max(invincTimer - deltaTime, 0.0f);

            if (invincTimer > 0.0f)
            {
                sprite.Color = new Color(255, 255, 255, 127);
            }else
            {
                sprite.Color = new Color(255, 255, 255, 255);
            }

            //Side borders
            if (Position.X > Program.ScreenW - sprite.Origin.X) newPos.X = Program.ScreenW - sprite.Origin.X; //Removed elses to fix border collision bugs
            
            if (newPos.X < 0 + sprite.Origin.X) newPos.X = 0 + sprite.Origin.X;

            //Upper and lower borders
            if (newPos.Y > Program.ScreenH - sprite.Origin.Y) newPos.Y = Program.ScreenH - sprite.Origin.Y;
            
            if (newPos.Y < 0 + sprite.Origin.Y) newPos.Y = 0 + sprite.Origin.Y;
            
            Position = newPos;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Right)) Position += new Vector2f(1, 0) * shipSpeed * deltaTime;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Left)) Position += new Vector2f(-1, 0) * shipSpeed * deltaTime;
            
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up)) Position += new Vector2f(0, -1) * shipSpeed * deltaTime;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down)) Position += new Vector2f(0, 1) * shipSpeed * deltaTime;


            if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && fireRateTimer > fireRate)
            {
                scene.Spawn(new Bullet(new Vector2f(0, -1), this)
                {
                    Position = this.Position - new Vector2f(0, this.sprite.Origin.Y)
                }); 
                
                fireRateTimer = 0;
            } 
            
            
            base.Update(scene, deltaTime);
        }

        public override void Render(RenderTarget target)
        {
            base.Render(target);
        }
    }
}
