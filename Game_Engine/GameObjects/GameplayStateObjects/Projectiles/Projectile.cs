using Game_Engine.Engine.Animation;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game_Engine.GameObjects.GameplayStateObjects.Projectiles
{
    public class Projectile : BaseGameObject, IGameObjectWidthDamage
    {
        private const float PROJECTILE_SPEED = 5.0f;
        private const float RADIUS = 10f;
        private const int PLAYER_POSITION_OFFSET = 32;

        private Animation _animation;
        private Vector2 _targetPosition;

        public int Damage => 100;

        public bool IsActive { get; set; }


        public Projectile(Animation animation, Vector2 position, Vector2 targetPosition)
        {
            _animation = animation;
            Position = new Vector2(position.X + PLAYER_POSITION_OFFSET, position.Y + PLAYER_POSITION_OFFSET);
            _targetPosition = targetPosition;
            _origin = new Vector2(animation.CurrentFrame.Width / 2, animation.CurrentFrame.Height / 2);
            _direction = targetPosition - _position;
            _direction.Normalize();
            _angle = (float)Math.Atan2(_direction.Y, _direction.X);
            IsActive = true;
            AddCircleCollider(new Engine.GameObjects.Collision_Detection.CircleColider(_origin, RADIUS));
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
