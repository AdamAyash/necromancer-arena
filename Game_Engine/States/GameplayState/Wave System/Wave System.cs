using System;
using Microsoft.Xna.Framework;
using Game_Engine.Engine.States;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Game_Engine.GameObjects.GameplayStateTextObjects;

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

        public event EventHandler<string> OnDisplayText;
        public event EventHandler OnBossFightSoudnEffect;
        public event EventHandler OnGameOver;
        public Wave_System(List<Wave> waves)
        {
            _waves = waves;
            _numberOFWaves = _waves.Count;
            _waveIndex = -1;
            SwitchToNextWave();
        }

        public void Update(GameTime gameTime, int currentAliveEnemies)
        {
            if ((float)gameTime.TotalGameTime.TotalSeconds - _timeElapsed >= _currentWave.EnemySpawnInterval.TotalSeconds)
            {
                _timeElapsed = (float)gameTime.TotalGameTime.TotalSeconds;
                if (_currentWaveListIndex <= _currentWaveListCount - 1 && _enemiesSpawned <= _currentWaveListEnemiesCount - 1)
                {
                    switch (_currentWave.EnemyTypesList[_currentWaveListIndex].Item1)
                    {
                        case EnemyTypes.DemonEnemy:
                            OnSpawnEnemies.Invoke(EnemyTypes.DemonEnemy, new GameplayStateEvents.SpawnDemonEnemy());
                            break;
                        case EnemyTypes.OldWizard:
                            OnSpawnEnemies.Invoke(EnemyTypes.OldWizard, new GameplayStateEvents.SpawnOldWizard());
                            break;
                        case EnemyTypes.Zombie:
                            OnSpawnEnemies.Invoke(EnemyTypes.Zombie, new GameplayStateEvents.SpawnZombie());
                            break;
                        case EnemyTypes.DemonBoss:
                            OnSpawnEnemies.Invoke(EnemyTypes.DemonBoss, new GameplayStateEvents.SpawnDemonBoss());
                            break;
                    }
                    _enemiesSpawned++;
                }
                else if (_currentWaveListIndex + 1 <= _currentWaveListCount - 1)
                {
                    _currentWaveListIndex++;
                    _currentWaveListEnemiesCount = _currentWave.EnemyTypesList[_currentWaveListIndex].Item2;
                    _enemiesSpawned = 0;
                }
                else
                {
                    if (currentAliveEnemies == 0)
                    {
                        SwitchToNextWave();
                    }
                }
            }
        }

        private void SwitchToNextWave()
        {
            _waveIndex++;
            if (_waveIndex < _numberOFWaves)
            {
                _currentWave = _waves[_waveIndex];
                _currentWaveListCount = _currentWave.EnemyTypesList.Count;
                _currentWaveListIndex = 0;
                _enemiesSpawned = 0;
                _currentWaveListEnemiesCount = _currentWave.EnemyTypesList[_currentWaveListIndex].Item2;
                if (_waveIndex == _waves.Count - 1)
                {
                    OnBossFightSoudnEffect?.Invoke(this, EventArgs.Empty);
                    OnDisplayText?.Invoke(this, $"Boss Fight");
                }
                else
                {
                    OnDisplayText?.Invoke(this, $"Wave {_waveIndex + 1}");
                }
            }
            else
            {
                OnGameOver?.Invoke(this, EventArgs.Empty);
            }
        }

       
    }
}
