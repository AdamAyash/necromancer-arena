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

        private const int randomEnemySpawnOffset = 96;

        private Animation _plyerProjectileAnimation;
        private Animation _oldWizardProjectileAnimation;

        private RandomNumberGenerator _rnd;

        private List<Animation> _playerAnimationList;
        private List<Animation> _demonAnimationList;
        private List<Animation> _oldWizardAnimationList;

        private Wave_System _waveSystem;
        private List<Wave> _waveList;

        private TiledMap _map;
        private TiledMapRenderer _mapRendered;

        private List<Projectile> _playerProjectilesList;
        private List<Projectile> _enemyProjectileList;

        private List<AI> _enemyList;

        private Player _player;
        private TimeSpan playerShootCooldown = TimeSpan.FromSeconds(1.0f);
        private TimeSpan lastShotAt;

        private Debug_Details _debugDetails;

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

            AddGameObject(_player);

            _playerProjectilesList = new List<Projectile>();
            _plyerProjectileAnimation = new Animation(LoadTexture(PLAYER_PROJECTILE_ANIMATION), 29, 40);

            _enemyProjectileList = new List<Projectile>();
            _oldWizardProjectileAnimation = new Animation(LoadTexture(OLD_WIZARD_PROJECTILE_ANIMATION), 28, 40);

            _enemyList = new List<AI>();

            _demonAnimationList = new List<Animation>();
            _demonAnimationList.Add(new Animation(LoadTexture(DEMON_RUN_ANIMATION), 4, 8));

            _oldWizardAnimationList = new List<Animation>();
            _oldWizardAnimationList.Add(new Animation(LoadTexture(OLD_WIZARD_IDLE_ANIMATION), 4, 8));

            _waveList = new List<Wave>()
            {
                new Wave(6)
            };
            _waveList[0].EnemyTypesList = new List<(EnemyTypes, int)>
            {
                (EnemyTypes.DemonEnemy,2),
                (EnemyTypes.OldWizard,2)
            };
            _waveSystem = new Wave_System(_waveList);
            _waveSystem.OnSpawnEnemies += _waveSystem_OnSpawnEnemies;

            _map = LoadTiledMap(MAP_ASSET_NAME);
            _mapRendered = new TiledMapRenderer(_graphicsDevice, _map);
            _rnd = new RandomNumberGenerator();
            SetInputManager(new GameplayInputMapper());

        }

        private void _waveSystem_OnSpawnEnemies(object sender, BaseGameStateEvent e)
        {
            if (e is GameplayStateEvents.SpawnDemonEnemy)
            {
                var randomEnemyPosition = new Vector2(_rnd.GenerateRandomInteger(randomEnemySpawnOffset, _graphicsDevice.Viewport.Width - randomEnemySpawnOffset), _rnd.GenerateRandomInteger(randomEnemySpawnOffset, _graphicsDevice.Viewport.Height - randomEnemySpawnOffset));
                var demonEnemy = new Demon_Enemy(_demonAnimationList, randomEnemyPosition);
                _enemyList.Add(demonEnemy);
                AddGameObject(demonEnemy);
            }
            else if(e is GameplayStateEvents.SpawnOldWizard)
            {
                var randomEnemyPosition = new Vector2(_rnd.GenerateRandomInteger(randomEnemySpawnOffset, _graphicsDevice.Viewport.Width - randomEnemySpawnOffset), _rnd.GenerateRandomInteger(randomEnemySpawnOffset, _graphicsDevice.Viewport.Height - randomEnemySpawnOffset));
                var oldWizardEnemy = new OldWizard_Enemy(_oldWizardAnimationList, randomEnemyPosition);
                oldWizardEnemy.OnEnemyShoot += OldWizardEnemy_OnEnemyShoot;
                _enemyList.Add(oldWizardEnemy);
                AddGameObject(oldWizardEnemy);
            }
        }

        private void OldWizardEnemy_OnEnemyShoot(object sender, Vector2 e)
        {
            var projectile = new Projectile(_oldWizardProjectileAnimation,e,_player.Position);
            _enemyProjectileList.Add(projectile);
            AddGameObject(projectile);
        }

        private void DetectCollisions()
        {
            var playerEnemyCollision = new AABBCollisionDetection<AI, Player>(_enemyList);
            var enemyProjectileCollision = new AABBCollisionDetection<Projectile, AI>(_playerProjectilesList);
            var enemyToEnemyCollision = new AABBCollisionDetection<AI, AI>(_enemyList);

            playerEnemyCollision.DetectCollision(_player, (enemy, player) =>
            {
                player.PlayerHealth -= enemy.Damage;
            });


            enemyProjectileCollision.DetectCircleCollision(_enemyList, (projectile, enemy) =>
             {
                 enemy.Health -= projectile.Damage;
             });

            enemyToEnemyCollision.DetectCollision((enemy1, enemy2) =>
            {
                enemy1.Speed += (float)_rnd.GenerateRandomFloat();
            });
        }
        protected void ShootProjectile(Vector2 mousePosition, GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds - lastShotAt.TotalSeconds >= playerShootCooldown.TotalSeconds)
            {
                var projectile = new Projectile(_plyerProjectileAnimation, _player.Position, mousePosition);
                _playerProjectilesList.Add(projectile);
                AddGameObject(projectile);
                lastShotAt = gameTime.TotalGameTime;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (_debugMode)
            {
                _debugDetails.Update(gameTime);
            }
            HandleInput(gameTime);
            InputManager.Update();
            _mapRendered.Update(gameTime);
            DetectCollisions();
            _waveSystem.Update(gameTime);

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

            foreach (var gameObject in _gameObjectsList)
            {
                gameObject.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _mapRendered.Draw();
            base.Draw(_spriteBatch);
        }

    }
}
