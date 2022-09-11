using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Game_Engine.Engine.Animation;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game_Engine.GameObjects.GameplayStateObjects.Player
{
    public class Player : BaseGameObject
    {
        private const float PLAYER_SPEED = 4.5f;
        private const  int CUURENT_ANIMATION_WIDTH = 48;

        private bool _isAlive;
        private int _hitAt;

        private Animation _currentAnimation;
        
        public List<Animation> Animations { get; set; }
        public int PlayerHealth { get; set; }
        public SpriteEffects PlayerOrientation { private get; set; }

        private TimeSpan playedTakesDamageInterval;
        private float lastTimePlayedGotDamaged;

        public Vector2 OriginPosition
        {
            get
            {
                return new Vector2(_position.X + (CUURENT_ANIMATION_WIDTH / 2),
                    _position.Y + (_currentAnimation.AnimationTexture.Height / 2));
            }
        }

        public bool IsAlive
        {
            get
            {
                return _isAlive;
            }
        }
        public Player(List<Animation> animationList)
        {
            Animations = animationList;
            _currentAnimation = Animations[0];
            _isAlive = false;
            PlayerOrientation = SpriteEffects.None;
            PlayerHealth = 6;
            playedTakesDamageInterval = TimeSpan.FromSeconds(1);
            lastTimePlayedGotDamaged = 0;
            _isAlive = true;
            _hitAt = 100;
            AddBoundingBoxes(new Engine.GameObjects.BoundingBox(Position, _currentAnimation.CurrentFrame.Width, _currentAnimation.CurrentFrame.Height));
        }
        
        public void TakeDamage(IGameObjectWidthDamage o, GameTime gameTime)
        {
            if ((float)gameTime.TotalGameTime.TotalSeconds - lastTimePlayedGotDamaged > playedTakesDamageInterval.TotalSeconds)
            {
                lastTimePlayedGotDamaged = (float)gameTime.TotalGameTime.TotalSeconds;
                PlayerHealth -= o.Damage;
                _hitAt = 0;
            }

            if (PlayerHealth <= 0)
            {
                _isAlive = true;
            }
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

        private Color GetColor()
        {
            var color = Color.White;
            foreach (var flashStartEndFrames in GetFlashStartEndFrames())
            {
                if (_hitAt >= flashStartEndFrames.Item1 && _hitAt < flashStartEndFrames.Item2)
                {
                    color = Color.Red;
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
            Color currentColor = GetColor();
            _spriteBatch.Draw(_currentAnimation.AnimationTexture, new Rectangle((int)Position.X, (int)Position.Y, _currentAnimation.CurrentFrame.Width, _currentAnimation.CurrentFrame.Height), _currentAnimation.CurrentFrame, currentColor, _angle, _origin, PlayerOrientation, 0f);
        }
    }
}
