using System;
using Game_Engine.Engine;
using Microsoft.Xna.Framework;
using Game_Engine.Engine.States;
using System.Collections.Generic;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Game_Engine.GameObjects.GameplayStateObjects.Player;
using Game_Engine.GameObjects.GameplayStateObjects.Projectiles;

namespace Game_Engine.States
{
    public class GameplayState : BaseGameState
    {
        private const string PLAYER_TEXTURE_NAME = "Assets/Animations/Player/Player";
        private List<Projectile> _projectilesList;
        private Player _player;

        public override void HandleCollision()
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
                    ShootProjectile(leftMouseButton.MousePosition);
                }
            });
        }

        public override void LoadContent()
        {
            SetInputManager(new GameplayInputMapper());
            _player = new Player(LoadTexture(PLAYER_TEXTURE_NAME));
            _projectilesList = new List<Projectile>();

            AddGameObject(_player);
        }

        protected void ShootProjectile(Vector2 mousePosition)
        {
            
        }


        public override void Update(GameTime gameTime)
        {
            HandleCollision();
            foreach (var gameObject in _gameObjectsList)
            {
                gameObject.Update(gameTime);
            }
        }
    }
}
