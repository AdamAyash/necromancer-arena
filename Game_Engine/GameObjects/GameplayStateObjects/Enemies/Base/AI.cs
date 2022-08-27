using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Game_Engine.Engine.Animation;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.GameObjects.GameplayStateObjects.Enemies
{
    public class AI : BaseGameObject, IGameObjectWidthDamage
    {
        private  int offscreenYPos = -150;
        private float _falldownSpeed = 5.5f;
        private  float _falldownAcceleration = 0.1f;

        protected float _speed;


        public List<Animation> _animations;
        protected Animation _currentAnimation;

        private bool _collides;

        protected bool _attackMode;

        private SpriteEffects _orinetation;

        public int Damage => 10;

        public int Health { get; set; }

        private float _yCordinate;

        public float Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                if (!_collides)
                {
                    _speed = value;
                    _collides = true;
                }
            }
        }


        public AI(List<Animation> animations, Vector2 position)
        {
            Position = new Vector2(position.X, offscreenYPos);
            _yCordinate = position.Y;
            _animations = animations;
            _collides = false;
            _attackMode = false;
        }

        public virtual void Update(GameTime gameTime, Vector2 playerPosition)
        {
            if (Position.Y < _yCordinate && !_attackMode)
            {
                Position = new Vector2(Position.X, Position.Y + _falldownSpeed);
                _falldownSpeed += _falldownAcceleration;

                return;
            }
            else
            {
                _attackMode = true;
            }
            if (_position.X > playerPosition.X)
            {
                _orinetation = SpriteEffects.FlipHorizontally;
            }
            else
            {
                _orinetation = SpriteEffects.None;
            }
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_currentAnimation.AnimationTexture, new Rectangle((int)_position.X, (int)_position.Y, _currentAnimation.CurrentFrame.Width, _currentAnimation.CurrentFrame.Height), _currentAnimation.CurrentFrame, Color.White, _angle, _origin, _orinetation, 0f);
        }
    }
}
