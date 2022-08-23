using System;
using Microsoft.Xna.Framework;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Game_Engine.Engine.Animation;

namespace Game_Engine.GameObjects.GameplayStateObjects.Projectiles
{
    public class Projectile : BaseGameObject,IGameObjectWidthDamage
    {
        private const float PROJECTILE_SPEED = 5.0f;
        private const float RADIUS = 30f;
        private const int PLAYER_POSITION_OFFSET = 32;

        private Animation _animation;
        private Vector2 _mousePosition;

        public int Damage => 10;

        public Projectile(Animation animation, Vector2 playerPosition, Vector2 mousePosition)
        {
            _animation = animation;
            Position = new Vector2(playerPosition.X + PLAYER_POSITION_OFFSET, playerPosition.Y + PLAYER_POSITION_OFFSET);
            _mousePosition = mousePosition;
            _origin = new Vector2(animation.CurrentFrame.Width/ 2, animation.CurrentFrame.Height / 2);
            _direction = mousePosition - _position;
            _direction.Normalize();
            _angle = (float)Math.Atan2(_direction.Y, _direction.X);
            AddCircleCollider(new Engine.GameObjects.Collision_Detection.CircleColider(_origin,RADIUS));
        }

        public override void Update(GameTime gameTime)
        {
            Position += _direction * PROJECTILE_SPEED;
            _animation.Update(gameTime);
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_animation.AnimationTexture, new Rectangle((int)Position.X, (int)Position.Y, _animation.CurrentFrame.Width, _animation.CurrentFrame.Height), _animation.CurrentFrame, Color.White, _angle, _origin, SpriteEffects.None, 0f);
        }
    }
}
