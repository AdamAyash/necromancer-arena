using System;
using Microsoft.Xna.Framework;
using Game_Engine.Engine.States;
using System.Collections.Generic;

namespace Game_Engine.States
{
    public class Wave_System
    {
        private List<Wave> _waves;
        private Wave _currentWave;

        private int _numberOFWaves;
        private int _waveIndex;
        private int _currentWaveListIndex;
        private int _currentWaveListCount;

        private int _currentWaveListEnemiesCount;
        private int _enemiesSpawned;

        private float _timeElapsed;

        public event EventHandler<BaseGameStateEvent> OnSpawnEnemies;
        public Wave_System(List<Wave> waves)
        {
            _waves = waves;
            _numberOFWaves = _waves.Count;
            _waveIndex = 0;
            _currentWave = _waves[_waveIndex];
            _currentWaveListCount = _currentWave.EnemyTypesList.Count;
            _currentWaveListIndex = 0;
            _enemiesSpawned = 0;
            _currentWaveListEnemiesCount = _currentWave.EnemyTypesList[_currentWaveListIndex].Item2;

        }
       
        public void Update(GameTime gameTime)
        {
            if ((float)gameTime.TotalGameTime.TotalSeconds - _timeElapsed >= _currentWave.EnemySpawnInterval.TotalSeconds)
            {
                _timeElapsed = (float)gameTime.TotalGameTime.TotalSeconds;
                if (_currentWaveListIndex < _currentWaveListCount)
                {
                    if (_enemiesSpawned < _currentWaveListEnemiesCount)
                    {
                        switch (_currentWave.EnemyTypesList[_currentWaveListIndex].Item1)
                        {
                            case EnemyTypes.DemonEnemy:
                                OnSpawnEnemies.Invoke(EnemyTypes.DemonEnemy, new GameplayStateEvents.SpawnDemonEnemy());
                                break;
                            case EnemyTypes.OldWizard:
                                OnSpawnEnemies.Invoke(EnemyTypes.OldWizard, new GameplayStateEvents.SpawnOldWizard());
                                break;


                        }
                        _enemiesSpawned++;
                    }
                    else
                    {
                        _currentWaveListIndex++;
                        _enemiesSpawned = 0;
                    }
                }
                else
                {
                    SwitchToNextWave();
                }
            }
        }

        private void SwitchToNextWave()
        {
            Console.WriteLine("Wave Switched");
        }

        public void AddWave(Wave wave)
        {
            _waves.Add(wave);
        }
    }
}
