using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Game_Engine.Engine.Animation;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.GameObjects.GameplayStateObjects.Player
{
    public class Player : BaseGameObject
    {
        private const float PLAYER_SPEED = 4.5f;
        private bool _isAlive;

        private Animation _currentAnimation;

        public List<Animation> Animations { get; set; }
        public int PlayerHealth { get; set; }
        public SpriteEffects PlayerOrientation { private get; set; }

        public Player(List<Animation> animationList)
        {
            Animations = animationList;
            _currentAnimation = Animations[0];
            _isAlive = true;
            PlayerOrientation = SpriteEffects.None;
            AddBoundingBoxes(new Engine.GameObjects.BoundingBox(Position, _currentAnimation.CurrentFrame.Width, _currentAnimation.CurrentFrame.Height));
            PlayerHealth = 100;
        }
        public void PlayerMoveLeft()
        {
            Position = new Vector2(_position.X - PLAYER_SPEED, _position.Y);
        }

        public void PlayerMoveRight()
        {
            Position = new Vector2(_position.X + PLAYER_SPEED, _position.Y);
        }
        public void PlayerMoveUp()
        {
            Position = new Vector2(_position.X, _position.Y - PLAYER_SPEED);
        }

        public void PlayerMoveDown()
        {
            Position = new Vector2(_position.X, _position.Y + PLAYER_SPEED);
        }

        public override void Update(GameTime gameTime)
        {
            _currentAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_currentAnimation.AnimationTexture, new Rectangle((int)Position.X, (int)Position.Y,_currentAnimation.CurrentFrame.Width, _currentAnimation.CurrentFrame.Height), _currentAnimation.CurrentFrame, Color.White, _angle, _origin, PlayerOrientation, 0f);
        }
    }
}
