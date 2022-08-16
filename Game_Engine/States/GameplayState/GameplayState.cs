using System;
using Game_Engine.Engine;
using Microsoft.Xna.Framework;
using Game_Engine.Engine.States;
using System.Collections.Generic;
using Game_Engine.Engine.Animation;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Game_Engine.GameObjects.GameplayStateObjects.Player;
using Game_Engine.GameObjects.GameplayStateObjects.Projectiles;

namespace Game_Engine.States
{
    public class GameplayState : BaseGameState
    {
        private const string PLAYER_IDLE_ANIMATION = "Assets/Animations/Player/Idle/Player_Idle";

        private List<Projectile> _projectilesList;
        private Player _player;

        private TimeSpan playerShootCooldown = TimeSpan.FromSeconds(1.0f);
        private TimeSpan lastShotAt;

        private Texture2D projectileTexture;
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
                    ShootProjectile(leftMouseButton.MousePosition,gameTime);
                }
            });
        }
        public override void LoadContent()
        {
            SetInputManager(new GameplayInputMapper());
            _player = new Player();
            _player.Animations.Add(new Animation(LoadTexture(PLAYER_IDLE_ANIMATION), 4, 8));

            _projectilesList = new List<Projectile>();
            projectileTexture = LoadTexture(string.Empty);

            AddGameObject(_player);
        }

        protected void ShootProjectile(Vector2 mousePosition,GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds - lastShotAt.TotalSeconds >= playerShootCooldown.TotalSeconds)
            {
                var projectile = new Projectile(projectileTexture, _player.Position, mousePosition);
                _projectilesList.Add(projectile);
                AddGameObject(projectile);
                lastShotAt = gameTime.TotalGameTime;
            }
        }

        public override void Update(GameTime gameTime)
        {
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
