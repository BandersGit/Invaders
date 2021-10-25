using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class GUI : Entity
    {
        private Text scoreText;
        private int maxHealth = 3;
        private int currentHealth;
        private int currentScore;
        
        public GUI() : base("fullHeart")
        {
            
        }

        public override void Create(Scene scene)
        {
            base.Create(scene);
            scoreText =  new Text();
            scoreText.Font = scene.Assets.LoadFont("future_thin");
            scoreText.DisplayedString = $"Score: {currentScore}";
            sprite.Origin = new Vector2f(sprite.TextureRect.Width / 2, sprite.TextureRect.Height / 2);
            sprite.Scale = new Vector2f(0.5f, 0.5f);
            scoreText.FillColor = Color.White;
            currentHealth = maxHealth;
            scene.Events.LoseHealth += OnLoseHealth;//Runs these local methods when the subscribed events happen
            scene.Events.GainScore += OnGainScore;  //
        }

        public override void Destroy(Scene scene)
        {
            scene.Events.LoseHealth -= OnLoseHealth;
            scene.Events.GainScore -= OnGainScore;
        }

        private void OnLoseHealth(Scene scene, int amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                scene.Reload = true;
            }
        }

        private void OnGainScore(Scene scene, int amount)
        {
            currentScore += amount;
            scoreText.DisplayedString = $"Score: {currentScore}";
        }

        public override void Render(RenderTarget target)
        {
            sprite.Position = new Vector2f(30, 40);
            scoreText.CharacterSize = 36;
            scoreText.Scale = new Vector2f(0.8f, 0.8f);

            for (int i = 0; i < maxHealth; i++)
            {
                sprite.TextureRect = i < currentHealth 
                    ? new IntRect(0, 0, 128, 128) 
                    : new IntRect(0, 0, 0, 0);
                base.Render(target);
                sprite.Position += new Vector2f(25, 0);
            }
            
            scoreText.Position = new Vector2f(480 - scoreText.GetGlobalBounds().Width, 25);
            target.Draw(scoreText);
        }
    }
}
