using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Game_Engine.Engine.Animation;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Game_Engine.GameObjects.GameplayStateObjects.Projectiles;

namespace Game_Engine.GameObjects.GameplayStateObjects.Enemies
{
    public class AI : BaseGameObject, IGameObjectWidthDamage
    {
        private int _offscreenYPos;
        private float _falldownSpeed;
        private float _falldownAcceleration;

        protected float _speed;

        private int _hitAt;

        private float _opacity;
        private float _opacityFadingRate;


        public List<Animation> _animations;
        protected Animation _currentAnimation;

        private bool _collides;

        protected bool _attackMode;

        private SpriteEffects _orinetation;


        public int Health { get; set; }

        public bool IsAlive { get; set; }

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

        public bool AttackMode
        {
            get
            {
                return _attackMode;
            }
        }

        public int Damage { get; set; }

        public AI(List<Animation> animations, Vector2 position)
        {
            Position = new Vector2(position.X, _offscreenYPos);
            _yCordinate = position.Y;
            _animations = animations;
            _collides = false;
            _attackMode = false;
            IsAlive = true;
            _opacity = 1f;
            _opacityFadingRate = 0.01f;
            _offscreenYPos = -150;
            _falldownSpeed = 5.5f;
            _falldownAcceleration = 0.1f;
            _hitAt = 100;
        }

        public virtual void Update(GameTime gameTime, Vector2 playerPosition)
        {
            if (Health <= 0)
            {
                _attackMode = false;
                _opacity -= _opacityFadingRate;
                if (_opacity <= 0)
                {
                    IsAlive = false;
                }

                return;
            }
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

        public void JustHit(IGameObjectWidthDamage o)
        {
            Projectile projectile = (Projectile)o;
            if (projectile.IsActive)
            {
                this.Health -= o.Damage;
                _hitAt = 0;
            }
        }

        private Color GetColor()
        {
            var color = Color.White;
            foreach (var flashStartEndFrames in GetFlashStartEndFrames())
            {
                if (_hitAt >= flashStartEndFrames.Item1 && _hitAt < flashStartEndFrames.Item2)
                {
                    color = Color.OrangeRed;
                }
            }

            _hitAt++;
            return color;
        }

        private List<(int, int)> GetFlashStartEndFrames()
        {
            return new List<(int, int)>
            {
                (0, 3),
                (10, 13)
            };
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            var color = GetColor();
            _spriteBatch.Draw(_currentAnimation.AnimationTexture, new Rectangle((int)_position.X, (int)_position.Y, _currentAnimation.CurrentFrame.Width, _currentAnimation.CurrentFrame.Height), _currentAnimation.CurrentFrame, color * _opacity, _angle, _origin, _orinetation, 0f);
        }
    }
}
