using System;
using Microsoft.Xna.Framework;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Game_Engine.Engine.Animation;

namespace Game_Engine.GameObjects.GameplayStateObjects.Projectiles
{
    public class Projectile : BaseGameObject
    {
        private const float PROJECTILE_SPEED = 5.0f;
        private const int PLAYER_POSITION_OFFSET = 32;

        private Animation _animation;

        private Vector2 _mousePosition;

        public Projectile(Animation animation, Vector2 playerPosition, Vector2 mousePosition)
        {
            _animation = animation;
            _position = playerPosition;
            _position.X += PLAYER_POSITION_OFFSET;
            _position.Y += PLAYER_POSITION_OFFSET;
            _mousePosition = mousePosition;
            _origin = new Vector2(animation.AnimationTexture.Width/ 2, animation.AnimationTexture.Height / 2);
            _direction = mousePosition - _position;
            _direction.Normalize();
            _angle = (float)Math.Atan2(_direction.Y, _direction.X);
        }

        public override void Update(GameTime gameTime)
        {
            _position += _direction * PROJECTILE_SPEED;
            _animation.Update(gameTime);
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_animation.AnimationTexture, new Rectangle((int)_position.X, (int)_position.Y, _animation.CurrentFrame.Width, _animation.CurrentFrame.Height), _animation.CurrentFrame, Color.White, _angle, _origin, SpriteEffects.None, 0f);
        }
    }
}
