using System;
using SFML.System;
using SFML.Graphics;

namespace Invaders
{
    public class Background : Entity
    {
        public Background() : base("blueBackground")
        {
            
        }

        public override void Render(RenderTarget target) //Uses the view bounds to render the background
        {
            View view = target.GetView();
            Vector2f topLeft = view.Center - 0.5f * view.Size;

            int tilesX = (int) MathF.Ceiling(view.Size.X / sprite.TextureRect.Width);
            int tilesY = (int) MathF.Ceiling(view.Size.Y / sprite.TextureRect.Height);

            for (int row = 0; row <= tilesY; row++)
            {
                for (int col = 0; col <= tilesX; col++)
                {
                    sprite.Position = topLeft + sprite.TextureRect.Width * new Vector2f(col, row);
                    target.Draw(sprite);
                }

            }
        }
        
    }
}
