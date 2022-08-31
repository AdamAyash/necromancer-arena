using System;
using System.Collections.Generic;

namespace Game_Engine.States
{
    public class Wave
    {
        public List<(EnemyTypes,int)> EnemyTypesList { get; set; }

        public TimeSpan EnemySpawnInterval { get; set; }
        public Wave(int enemySpawnInterval)
        {
            EnemySpawnInterval = TimeSpan.FromSeconds(enemySpawnInterval);
        }
    }
}
