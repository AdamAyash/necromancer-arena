using System;
using Game_Engine.Engine;
using Microsoft.Xna.Framework;
using Game_Engine.Engine.States;
using System.Collections.Generic;
using Game_Engine.Engine.Animation;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Game_Engine.Engine.GameObjects.Debug;
using Game_Engine.GameObjects.GameplayStateObjects.Player;
using Game_Engine.GameObjects.GameplayStateObjects.Enemies;
using Game_Engine.GameObjects.GameplayStateObjects.Projectiles;

namespace Game_Engine.States
{
    public class GameplayState : BaseGameState
    {
        private const string PLAYER_IDLE_ANIMATION = "Assets/Animations/Player/Idle/Player_Idle";
        private const string PROJECTILE_ANIMATION = "Assets/Animations/Player/Spells/Fireball_Spell";

        private const string DEMON_RUN_ANIMATION = "Assets/Animations/Enemies/Demon/Demon_Run";

        private Animation _projectileAnimation;

        private List<Animation> _playerAnimationList;
        private List<Animation> _demonAnimationList;


        private List<Projectile> _projectilesList;
        private List<AI> _enemyList;

        private Player _player;
        private TimeSpan playerShootCooldown = TimeSpan.FromSeconds(1.0f);
        private TimeSpan lastShotAt;

        private Debug_Details _debugDetails;

        public override void HandleCollision(GameTime gameTime)
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
            _debugMode = true;

            if (_debugMode)
            {
                _debugDetails = new Debug_Details(LoadSpriteFont(string.Empty));
                AddGameObject(_debugDetails);
            }
            _playerAnimationList = new List<Animation>();
            _playerAnimationList.Add(new Animation(LoadTexture(PLAYER_IDLE_ANIMATION), 4, 8));
            _player = new Player(_playerAnimationList);

            AddGameObject(_player);

            _projectilesList = new List<Projectile>();
            _projectileAnimation = new Animation(LoadTexture(PROJECTILE_ANIMATION), 2, 1);

            _enemyList = new List<AI>();

            _demonAnimationList = new List<Animation>();
            _demonAnimationList.Add(new Animation(LoadTexture(DEMON_RUN_ANIMATION), 4, 8));
            var enemy = new Demon_Enemy(_demonAnimationList);
            _enemyList.Add(enemy);
            AddGameObject(enemy);


            SetInputManager(new GameplayInputMapper());


        }

        protected void ShootProjectile(Vector2 mousePosition, GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds - lastShotAt.TotalSeconds >= playerShootCooldown.TotalSeconds)
            {
                var projectile = new Projectile(_projectileAnimation, _player.Position, mousePosition);
                _projectilesList.Add(projectile);
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
            HandleCollision(gameTime);
            InputManager.Update();

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
            base.Draw(_spriteBatch);
        }

    }
}
