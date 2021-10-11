using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Invaders
{
    public class Player : Entity
    {
        private const float ShipSpeed = 500.0f;

        public Player() : base("spaceSheet")
        {
            sprite.TextureRect = new IntRect(325, 0, 98, 75);
            sprite.Origin = new Vector2f(98f / 2, 75f / 2);
            Position = new Vector2f(125, 350);
            size = new Vector2f(sprite.GetGlobalBounds().Width, sprite.GetGlobalBounds().Height);
        }

        public override void Update(Scene scene, float deltaTime)
        {
            var newPos = Position;

            if (Position.X > Program.ScreenW - sprite.Origin.X)
            {
                newPos.X = Program.ScreenW - sprite.Origin.X;
            }
            if (newPos.X < 0 + sprite.Origin.X)
            {
                newPos.X = 0 + sprite.Origin.X;
            }
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
                scene.TryMove(this, new Vector2f(ShipSpeed * deltaTime, 0));
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                scene.TryMove(this, new Vector2f(-ShipSpeed * deltaTime, 0));
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                scene.TryMove(this, new Vector2f(0, -ShipSpeed * deltaTime));
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                scene.TryMove(this, new Vector2f(0, ShipSpeed * deltaTime));
            }
        }

        public override void Render(RenderTarget target)
        {
            base.Render(target);
        }
    }
}
