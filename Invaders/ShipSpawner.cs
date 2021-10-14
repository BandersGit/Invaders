using System;
using SFML.System;
using SFML.Graphics;

namespace Invaders
{
    public class ShipSpawner : Entity
    {
        private Random enemySpawn;
        private float enemySpawnRateTimer = 4000;
        private float enemyRateCap = 1.0f;
        private float enemyRateCapTimer = 0.0f;

        public ShipSpawner() : base("")
        {

        }

        public override void Create(Scene scene)
        {
            enemySpawn = new Random();
        }

        public override void Update(Scene scene, float deltaTime)
        {
            enemySpawnRateTimer -= deltaTime * 2;
            enemyRateCapTimer += deltaTime;

            if (enemySpawnRateTimer <= 2000) enemySpawnRateTimer = 2000;
            
            int spawnEnemy = enemySpawn.Next((int) enemySpawnRateTimer);

            if (spawnEnemy == 0 && enemyRateCapTimer > enemyRateCap)
            {
                int spawnLocation = enemySpawn.Next(3);

                if (spawnLocation == 0)
                {
                    scene.Spawn(new Enemy(){Position = new Vector2f(Program.ScreenW / 2, 0)});
                }else if (spawnLocation == 1)
                {
                    scene.Spawn(new Enemy());
                }else if (spawnLocation == 2)
                {
                    scene.Spawn(new Enemy(){Position = new Vector2f(Program.ScreenW, 0)});
                }
                
                enemyRateCapTimer = 0.0f;
            }
        }
    }
}
