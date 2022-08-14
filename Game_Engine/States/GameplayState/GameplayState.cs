using System;
using Microsoft.Xna.Framework;
using Game_Engine.Engine.States;
using Game_Engine.Engine.GameObjects;
using Game_Engine.GameObjects.GameplayStateObjects.Player;
using Microsoft.Xna.Framework.Graphics;
using Game_Engine.Engine;

namespace Game_Engine.States
{
    public class GameplayState : BaseGameState
    {
        private const string PLAYER_TEXTURE_NAME = "Assets/Animations/Player/Player";

        private Player _player;

        public override void HandleCollision()
        {
            InputManager.GetCommands(cmd =>
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
        }

        public override void LoadContent()
        {
            SetInputManager(new GameplayInputMapper());
            _player = new Player(LoadTexture(PLAYER_TEXTURE_NAME));
            AddGameObject(_player);
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
