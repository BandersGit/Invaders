using System;

namespace Invaders
{
    public delegate void ValueChangedEvent(Scene scene, int value);

    public class EventManager
    {
        public event ValueChangedEvent LoseHealth;
        public event ValueChangedEvent GainScore;

        private int healthLost;
        private int scoreGained;

        public void PublishLoseHealth(int amount) => healthLost += amount;
        public void PublishGainScore(int amount) => scoreGained += amount;

        public void HandleEvents(Scene scene)
        {
            if (healthLost != 0)
            {
                LoseHealth?.Invoke(scene, healthLost);
                healthLost = 0;
            }

            if (scoreGained != 0)
            {
                GainScore?.Invoke(scene, scoreGained);
                scoreGained = 0;
            }
        }

    }
}
