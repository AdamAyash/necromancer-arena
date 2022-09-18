using System;
using MonoGame.Extended;
using Game_Engine.Engine;
using Microsoft.Xna.Framework;
using Game_Engine.Engine.Common;
using MonoGame.Extended.Tiled;
using Game_Engine.Engine.States;
using System.Collections.Generic;
using Game_Engine.Engine.Animation;
using Game_Engine.Engine.GameObjects;
using MonoGame.Extended.Tiled.Renderers;
using Microsoft.Xna.Framework.Graphics;
using Game_Engine.Engine.GameObjects.Debug;
using Game_Engine.GameObjects.GameplayStateObjects.Player;
using Game_Engine.GameObjects.GameplayStateObjects.Enemies;
using Game_Engine.GameObjects.GameplayStateObjects.Projectiles;
using Game_Engine.GameObjects.GameplayStateTextObjects;
using Game_Engine.GameObjects.UI;
using Game_Engine.GameObjects.GameplayStateObjects.Enemies.Zombie;
using Game_Engine.GameObjects.GameplayStateObjects.Enemies.Bosses;
using Game_Engine.GameObjects.Potions;

namespace Game_Engine.States
{
    public class GameplayState : BaseGameState
    {
        private const string MAP_ASSET_NAME = "Assets/Maps/Map";

        private const string PLAYER_IDLE_ANIMATION = "Assets/Animations/Player/Idle/Player_Idle";
        private const string PLAYER_PROJECTILE_ANIMATION = "Assets/Animations/Player/Spells/Necromancer_Spell";
        private const string OLD_WIZARD_PROJECTILE_ANIMATION = "Assets/Animations/Enemies/OldWizard/Arcane_Spell";
        private const string DEMON_RUN_ANIMATION = "Assets/Animations/Enemies/Demon/Demon_Run";
        private const string OLD_WIZARD_IDLE_ANIMATION = "Assets/Animations/Enemies/OldWizard/Old_Wizard_Idle";
        private const string ZOMBIE_IDLE_ANIMATION = "Assets/Animations/Enemies/Zombie/zombie_Run";
        private const string DEMONBOSS_RUN_ANIMATION = "Assets/Animations/Enemies/Bosses/DemonBoss/DemonBoss_Run";
        private const string DEMONBOSS_SPELL_ANIMATION = "Assets/Animations/Enemies/Bosses/DemonBoss/demon_fire";

        private const string HEALTH_POTION_TEXTURE = "Assets/Sprites/flask_big_red";

        private const string DISPAY_TEXT_FONT = "Assets/Fonts/Wave_Switch/WaveSwitch";
        private const string GAMEOVER_TEXT_FONT = "Assets/Fonts/GameOver/GameOverText";

        private const string UI_PLAYERHEALTH_0 = "Assets/UI/PlayerHealth/hearths_0";
        private const string UI_PLAYERHEALTH_1 = "Assets/UI/PlayerHealth/hearths_1";
        private const string UI_PLAYERHEALTH_2 = "Assets/UI/PlayerHealth/hearths_2";
        private const string UI_PLAYERHEALTH_3 = "Assets/UI/PlayerHealth/hearths_3";
        private const string UI_PLAYERHEALTH_4 = "Assets/UI/PlayerHealth/hearths_4";
        private const string UI_PLAYERHEALTH_5 = "Assets/UI/PlayerHealth/heaths_5";
        private const string UI_PLAYERHEALTH_6 = "Assets/UI/PlayerHealth/hearths_6";

        private const string MAIN_SOUNDTRACK = "Assets/Audio/Soundtracks/MainSoundtrack";
        private const string HIT_SOUNDEFFECT = "Assets/Audio/Effects/Hit";
        private const string PLAYER_SHOOT_SOUNDEFFECT = "Assets/Audio/Effects/Shoot";
        private const string BOSS_FIGHT_SOUNDEFFECT = "Assets/Audio/Effects/BossFight";

        private const int PLAYERBOUNDSLEFT_X = 60;
        private const int PLAYERBOUNDSRIGHT_X = 1100;
        private const int PLAYERBOUNDSLEFT_Y = 60;
        private const int PLAYERBOUNDSRIGHT_Y = 600;

        private const int offScreenCleanObjectsTrigger = -500;

        private const int randomEnemySpawnOffset = 96;

        private Texture2D _healthPotionTexture;

        private SpriteFont _displayTextFont;

        private Animation _plyerProjectileAnimation;
        private Animation _oldWizardProjectileAnimation;
        private Animation _demonBossProjectileAnimation;

        private RandomNumberGenerator _rnd;

        private List<Animation> _playerAnimationList;
        private List<Animation> _demonAnimationList;
        private List<Animation> _oldWizardAnimationList;
        private List<Animation> _zombieAnimationList;
        private List<Animation> _demonBossAnimationList;

        private Wave_System _waveSystem;
        private List<Wave> _waveList;

        private TiledMap _map;
        private TiledMapRenderer _mapRendered;

        private List<Projectile> _playerProjectilesList;
        private List<Projectile> _enemyProjectileList;

        private List<HealthPotion> _healthPotionsList;

        private List<AI> _enemyList;

        private Player _player;
        private TimeSpan playerShootCooldown = TimeSpan.FromSeconds(1.0f);
        private TimeSpan lastShotAt;

        private Debug_Details _debugDetails;
        private GameOverText _gameOverText;

        private PlayerHealth _playerHealth;
        private Texture2D[] _playerHealthTextures;

        public override void HandleInput(GameTime gameTime)
        {
            InputManager.GetKeyboardCommands(cmd =>
            {
                if (cmd is GameplayInputCommands.PlayerMoveLeftCommand)
                {
                    _player.PlayerMoveLeft();
                }
                if (cmd is GameplayInputCommands.PlayerMoveRightCommand)
                {
                    _player.PlayerMoveRight();
                }
                if (cmd is GameplayInputCommands.PlayerMoveUpCommand)
                {
                    _player.PlayerMoveUp();
                }
                if (cmd is GameplayInputCommands.PlayerMoveDownCommand)
                {
                    _player.PlayerMoveDown();
                }
            });

            InputManager.GetMouseCommands(cmd =>
            {
                if (cmd is GameplayInputCommands.PlayerShootCommand)
                {
                    var leftMouseButton = (GameplayInputCommands.PlayerShootCommand)cmd;
                    ShootProjectile(leftMouseButton.MousePosition, gameTime);
                }
            });
        }
        public override void LoadContent()
        {
            _debugMode = false;

            if (_debugMode)
            {
                _debugDetails = new Debug_Details(LoadSpriteFont(string.Empty));
                AddGameObject(_debugDetails);
            }
            _playerAnimationList = new List<Animation>();
            _playerAnimationList.Add(new Animation(LoadTexture(PLAYER_IDLE_ANIMATION), 4, 8));
            _player = new Player(_playerAnimationList);
            _player.Position = new Vector2(_graphicsDevice.Viewport.Bounds.Width / 2, _graphicsDevice.Viewport.Bounds.Height / 2);
            _player.OnPlayerHit += _player_OnPlayerHit;
            AddGameObject(_player);

            _healthPotionsList = new List<HealthPotion>();
            _playerProjectilesList = new List<Projectile>();
            _plyerProjectileAnimation = new Animation(LoadTexture(PLAYER_PROJECTILE_ANIMATION), 29, 40);

            _enemyProjectileList = new List<Projectile>();
            _oldWizardProjectileAnimation = new Animation(LoadTexture(OLD_WIZARD_PROJECTILE_ANIMATION), 28, 40);
            _demonBossProjectileAnimation = new Animation(LoadTexture(DEMONBOSS_SPELL_ANIMATION), 30, 40);

            _enemyList = new List<AI>();

            _demonAnimationList = new List<Animation>();
            _demonAnimationList.Add(new Animation(LoadTexture(DEMON_RUN_ANIMATION), 4, 8));

            _oldWizardAnimationList = new List<Animation>();
            _oldWizardAnimationList.Add(new Animation(LoadTexture(OLD_WIZARD_IDLE_ANIMATION), 4, 8));

            _zombieAnimationList = new List<Animation>();
            _zombieAnimationList.Add(new Animation(LoadTexture(ZOMBIE_IDLE_ANIMATION), 4, 8));

            _demonBossAnimationList = new List<Animation>();
            _demonBossAnimationList.Add(new Animation(LoadTexture(DEMONBOSS_RUN_ANIMATION), 4, 8));

            _healthPotionTexture = LoadTexture(HEALTH_POTION_TEXTURE);

            var mainSoundtrack = LoadSoundEffect(MAIN_SOUNDTRACK).CreateInstance();
            mainSoundtrack.Volume = 0.5f;
            _soundManager.SetSoundtrack(new List<Microsoft.Xna.Framework.Audio.SoundEffectInstance>() { mainSoundtrack });

            var hitSoundEffect = LoadSoundEffect(HIT_SOUNDEFFECT);
            var playerShootSoundEffect = LoadSoundEffect(PLAYER_SHOOT_SOUNDEFFECT);
            var bossFightSoundEffect = LoadSoundEffect(BOSS_FIGHT_SOUNDEFFECT);

            _soundManager.RegisterSound(new GameplayStateEvents.EntityHit(), hitSoundEffect);
            _soundManager.RegisterSound(new GameplayStateEvents.PlayerShoots(), playerShootSoundEffect);
            _soundManager.RegisterSound(new GameplayStateEvents.BossFight(), bossFightSoundEffect);

            _displayTextFont = LoadSpriteFont(DISPAY_TEXT_FONT);
            _gameOverText = new GameOverText(LoadSpriteFont(GAMEOVER_TEXT_FONT), new Vector2(_graphicsDevice.Viewport.Width / 2 - 200, 100));


            _waveList = new List<Wave>()
            {
                new Wave(3),
                new Wave(2),
                new Wave(2),
                new Wave(2),
            };
            _waveList[0].EnemyTypesList = new List<(EnemyTypes, int)>
            {
                (EnemyTypes.DemonEnemy,10),

            };
            _waveList[1].EnemyTypesList = new List<(EnemyTypes, int)>
            {
                (EnemyTypes.OldWizard,2),
                (EnemyTypes.DemonEnemy,1),
                (EnemyTypes.OldWizard,3),
                 (EnemyTypes.DemonEnemy,2),
                (EnemyTypes.OldWizard,3),
                (EnemyTypes.DemonEnemy,4),
           };
           _waveList[2].EnemyTypesList = new List<(EnemyTypes, int)>
            {
                (EnemyTypes.Zombie,1),
               (EnemyTypes.OldWizard,3),
                (EnemyTypes.DemonEnemy,2),
                  (EnemyTypes.Zombie,2),
                (EnemyTypes.OldWizard,3),
                 (EnemyTypes.DemonEnemy,4),
            };
            _waveList[3].EnemyTypesList = new List<(EnemyTypes, int)>
            {
                (EnemyTypes.DemonBoss,1)
            };
            _waveSystem = new Wave_System(_waveList);
            _waveSystem_OnDisplayText(this,"Wave 1");
            _waveSystem.OnSpawnEnemies += _waveSystem_OnSpawnEnemies;
            _waveSystem.OnDisplayText += _waveSystem_OnDisplayText;
            _waveSystem.OnBossFightSoudnEffect += _waveSystem_OnBossFightSoudnEffect;

            _map = LoadTiledMap(MAP_ASSET_NAME);
            _mapRendered = new TiledMapRenderer(_graphicsDevice, _map);
            _rnd = new RandomNumberGenerator();

            _playerHealthTextures = new Texture2D[]
            {
                LoadTexture(UI_PLAYERHEALTH_0),
                LoadTexture(UI_PLAYERHEALTH_1),
                LoadTexture(UI_PLAYERHEALTH_2),
                LoadTexture(UI_PLAYERHEALTH_3),
                LoadTexture(UI_PLAYERHEALTH_4),
                LoadTexture(UI_PLAYERHEALTH_5),
                LoadTexture(UI_PLAYERHEALTH_6),
            };


            _playerHealth = new PlayerHealth(_playerHealthTextures);
            AddGameObject(_playerHealth);
            SetInputManager(new GameplayInputMapper());

        }

        private void _waveSystem_OnBossFightSoudnEffect(object sender, EventArgs e)
        {
            _soundManager.OnNotify(new GameplayStateEvents.BossFight());
        }

        private void _player_OnPlayerHit(object sender, EventArgs e)
        {
            _soundManager.OnNotify(new GameplayStateEvents.EntityHit());
        }

        private void _waveSystem_OnDisplayText(object sender, string e)
        {
            var waveText = new DisplayText(_displayTextFont, e, new Vector2(550, 70));
            AddGameObject(waveText);
        }


        private void _waveSystem_OnSpawnEnemies(object sender, BaseGameStateEvent e)
        {
            if (e is GameplayStateEvents.SpawnDemonEnemy)
            {
                var randomEnemyPosition = new Vector2(_rnd.GenerateRandomInteger(randomEnemySpawnOffset, _graphicsDevice.Viewport.Width - randomEnemySpawnOffset), _rnd.GenerateRandomInteger(randomEnemySpawnOffset, _graphicsDevice.Viewport.Height - randomEnemySpawnOffset));
                var demonEnemy = new Demon_Enemy(_demonAnimationList, randomEnemyPosition);
                demonEnemy.OnEnemyDeath += AI_OnEnemyDeath;
                _enemyList.Add(demonEnemy);
                AddGameObject(demonEnemy);
            }
            else if (e is GameplayStateEvents.SpawnOldWizard)
            {
                var randomEnemyPosition = new Vector2(_rnd.GenerateRandomInteger(randomEnemySpawnOffset, _graphicsDevice.Viewport.Width - randomEnemySpawnOffset), _rnd.GenerateRandomInteger(randomEnemySpawnOffset, _graphicsDevice.Viewport.Height - randomEnemySpawnOffset));
                var oldWizardEnemy = new OldWizard_Enemy(_oldWizardAnimationList, randomEnemyPosition);
                oldWizardEnemy.OnEnemyShoot += OldWizardEnemy_OnEnemyShoot;
                oldWizardEnemy.OnEnemyDeath += AI_OnEnemyDeath;
                _enemyList.Add(oldWizardEnemy);
                AddGameObject(oldWizardEnemy);
            }
            else if (e is GameplayStateEvents.SpawnZombie)
            {
                var randomEnemyPosition = new Vector2(_rnd.GenerateRandomInteger(randomEnemySpawnOffset, _graphicsDevice.Viewport.Width - randomEnemySpawnOffset), _rnd.GenerateRandomInteger(randomEnemySpawnOffset, _graphicsDevice.Viewport.Height - randomEnemySpawnOffset));
                var zombie = new Zombie(_zombieAnimationList, randomEnemyPosition);
                zombie.OnEnemyDeath += AI_OnEnemyDeath;
                _enemyList.Add(zombie);
                AddGameObject(zombie);
            }
            else if (e is GameplayStateEvents.SpawnDemonBoss)
            {
                var centerPosition = new Vector2(_graphicsDevice.Viewport.Bounds.Width / 2, _graphicsDevice.Viewport.Bounds.Height / 2);
                var demonBoss = new DemonBoss(_demonBossAnimationList, centerPosition);
                demonBoss.OnBossMassShooting += DemonBoss_OnBossMassShooting;
                _enemyList.Add(demonBoss);
                AddGameObject(demonBoss);
            }
        }

        private void AI_OnEnemyDeath(object sender, Vector2 e)
        {
            HealthPotion healthPotion = new HealthPotion(_healthPotionTexture, e);
            _healthPotionsList.Add(healthPotion);
            AddGameObject(healthPotion);
        }

        private void DemonBoss_OnBossMassShooting(object sender, Vector2 e)
        {
            int numberOfProjectiles = 20;
            var angle = ((Math.PI * 2) / numberOfProjectiles);
            for (int i = 0; i < numberOfProjectiles; i++)
            {

                var targetPosition = new Vector2((float)Math.Cos(angle * i), (float)Math.Sin(angle * i));
                var projectile = new Projectile(_demonBossProjectileAnimation, targetPosition, e, true);
                _enemyProjectileList.Add(projectile);
                AddGameObject(projectile);
            }
            _soundManager.OnNotify(new GameplayStateEvents.PlayerShoots());
        }

        private void OldWizardEnemy_OnEnemyShoot(object sender, Vector2 e)
        {
            var projectile = new Projectile(_oldWizardProjectileAnimation, e, _player.OriginPosition);
            _enemyProjectileList.Add(projectile);
            AddGameObject(projectile);
            _soundManager.OnNotify(new GameplayStateEvents.PlayerShoots());
        }

        private void DetectCollisions(GameTime gameTime)
        {
            var playerEnemyCollision = new AABBCollisionDetection<AI, Player>(_enemyList);
            var enemyProjectileCollision = new AABBCollisionDetection<Projectile, AI>(_playerProjectilesList);
            var enemyToEnemyCollision = new AABBCollisionDetection<AI, AI>(_enemyList);
            var enemyProjectileToPlayerCollision = new AABBCollisionDetection<Projectile, Player>(_enemyProjectileList);
            var playerToHealthPotionCollision = new AABBCollisionDetection<HealthPotion, Player>(_healthPotionsList);

            playerEnemyCollision.DetectCollision(_player, (enemy, player) =>
            {
                if (enemy.AttackMode)
                {
                    player.TakeDamage(enemy, gameTime);
                    _playerHealth.PlayerHealthIndex = _player.PlayerHealth;
                }
            });


            enemyProjectileCollision.DetectCircleCollision(_enemyList, (projectile, enemy) =>
             {
                 if (enemy.AttackMode)
                 {
                     enemy.JustHit(projectile);
                     projectile.IsActive = false;
                     _soundManager.OnNotify(new GameplayStateEvents.EntityHit());
                 }
             });

            enemyToEnemyCollision.DetectCollision((enemy1, enemy2) =>
            {
                enemy1.Speed += (float)_rnd.GenerateRandomFloat();
            });

            enemyProjectileToPlayerCollision.DetectCircleCollision(_player, (projectile, player) =>
            {
                _player.TakeDamage(projectile, gameTime);
                _playerHealth.PlayerHealthIndex = _player.PlayerHealth;
                projectile.IsActive = false;
                _soundManager.OnNotify(new GameplayStateEvents.EntityHit());
            });

            playerToHealthPotionCollision.DetectCollision(_player, (healthPotion, player) =>
            {
                _player.PlayerHeal(healthPotion);
                _playerHealth.PlayerHealthIndex = _player.PlayerHealth;
                healthPotion.IsActive = false;
                _soundManager.OnNotify(new GameplayStateEvents.EntityHit());
            });

            KeepPlayerBounds();
        }
        protected void ShootProjectile(Vector2 mousePosition, GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds - lastShotAt.TotalSeconds >= playerShootCooldown.TotalSeconds)
            {
                _soundManager.OnNotify(new GameplayStateEvents.PlayerShoots());
                var projectile = new Projectile(_plyerProjectileAnimation, _player.Position, mousePosition);
                _playerProjectilesList.Add(projectile);
                AddGameObject(projectile);
                lastShotAt = gameTime.TotalGameTime;
            }
        }

        private void KeepPlayerBounds()
        {
            if (_player.Position.X < PLAYERBOUNDSLEFT_X)
            {

                _player.Position = new Vector2(PLAYERBOUNDSLEFT_X, _player.Position.Y);
            }
            if (_player.Position.X > PLAYERBOUNDSRIGHT_X)
            {
                _player.Position = new Vector2(PLAYERBOUNDSRIGHT_X, _player.Position.Y);
            }

            if (_player.Position.Y < PLAYERBOUNDSLEFT_Y)
            {

                _player.Position = new Vector2(_player.Position.X, PLAYERBOUNDSLEFT_Y);
            }
            if (_player.Position.Y > PLAYERBOUNDSRIGHT_Y)
            {
                _player.Position = new Vector2(_player.Position.X, PLAYERBOUNDSRIGHT_Y);
            }
        }

        private List<T> Clean<T>(List<T> someObjectList, Func<T, bool> predicate, Action<T> action) where T : BaseGameObject
        {
            List<T> newList = new List<T>();
            foreach (var obj in someObjectList)
            {
                if (predicate(obj))
                {
                    action(obj);
                }
                else
                {
                    newList.Add(obj);
                }
            }
            return newList;
        }

        private bool ProjectilePredicate(Projectile pro)
        {
            if (!pro.IsActive || pro.Position.X > _graphicsDevice.Viewport.Width
                || pro.Position.X < offScreenCleanObjectsTrigger || pro.Position.Y > _graphicsDevice.Viewport.Height
                || pro.Position.Y < offScreenCleanObjectsTrigger)
            {
                return true;
            }

            return false;
        }

        private bool PotionPredicate(HealthPotion potion)
        {
            if (!potion.IsActive)
            {
                return true;
            }

            return false;
        }

        private bool EnemyPredicate(AI enemy)
        {
            if (!enemy.IsAlive)
            {
                return true;
            }

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            if (_debugMode)
            {
                _debugDetails.Update(gameTime);
            }
            if (_player.IsAlive)
            {
                HandleInput(gameTime);
                InputManager.Update();
                _soundManager.PlaySoundtrack();
                _mapRendered.Update(gameTime);
                DetectCollisions(gameTime);

                if (InputManager.MouseX < _player.Position.X)
                {
                    _player.PlayerOrientation = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    _player.PlayerOrientation = SpriteEffects.None;
                }

                foreach (var enemy in _enemyList)
                {
                    enemy.Update(gameTime, _player.Position);
                }
                _waveSystem.Update(gameTime, _enemyList.Count);

                foreach (var gameObject in _gameObjectsList)
                {
                    gameObject.Update(gameTime);
                }

                _playerProjectilesList = Clean<Projectile>(_playerProjectilesList, ProjectilePredicate, pro =>
                {
                    RemoveGameObject(pro);
                });

                _enemyList = Clean<AI>(_enemyList, EnemyPredicate, enemy =>
                {
                    RemoveGameObject(enemy);
                });

                _healthPotionsList = Clean<HealthPotion>(_healthPotionsList, PotionPredicate, potion =>
                {
                    RemoveGameObject(potion);
                });
                _enemyProjectileList = Clean<Projectile>(_enemyProjectileList, ProjectilePredicate, pro =>
                 {
                     RemoveGameObject(pro);
                 });
                    
            }
            else
            {
                AddGameObject(_gameOverText);
            }
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _mapRendered.Draw();
            base.Draw(_spriteBatch);
        }

    }
}
