using Game_Engine.Engine.GameObjects;
using Game_Engine.GameObjects.GameplayStateObjects.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;

namespace Game_Engine.GameObjects.Potions
{
    public class HealthPotion : BaseGameObject
    {
        public int Health { get; set; }
        private Timer _timer;
        private float _timerInterval;

        private float _opacity;
        private float _opacityFadingRate;

        public bool IsActive { get; set; }
        public HealthPotion(Texture2D texture,Vector2 enemyPosition)
        {
            _objectTexture = texture;
            AddBoundingBoxes(new Engine.GameObjects.BoundingBox(Position, _objectTexture.Width, _objectTexture.Height));
            Position = enemyPosition;
            Health = 2;
            IsActive = true;
            _timerInterval = 10000.0f;
            _timer = new Timer(_timerInterval);
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
            _opacity = 1f;
            _opacityFadingRate = 0.01f;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            IsActive = false;
        }


        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_objectTexture, new Rectangle((int)_position.X, (int)_position.Y, _objectTexture.Width, _objectTexture.Height), null, Color.White * _opacity, _angle, _origin, SpriteEffects.None, 0f);
        }
    }
}
