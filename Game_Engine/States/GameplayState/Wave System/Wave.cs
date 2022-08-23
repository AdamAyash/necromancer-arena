using System;
using System.Collections.Generic;

namespace Game_Engine.States
{
    public class Wave
    {
        public List<(EnemyTypes,int)> EnemyTypesList { get; set; } 
        public int EnemiesToSpawn { get; set; }
        public int EnemeiesSpawned { get; set; }

        public TimeSpan EnemySpawnInterval { get; set; }
        public Wave(int enemySpawnInterval)
        {
            EnemySpawnInterval = TimeSpan.FromSeconds(enemySpawnInterval);
            EnemeiesSpawned = 0;
        }
    }
}
