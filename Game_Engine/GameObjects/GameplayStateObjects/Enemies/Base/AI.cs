using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Game_Engine.Engine.Animation;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.GameObjects.GameplayStateObjects.Enemies
{
    public class AI : BaseGameObject
    {
        protected float _speed;

        public List<Animation> _animations;
        protected Animation _currentAnimation;

        private SpriteEffects _orinetation;
        public AI(List<Animation> animations)
        {
            _animations = animations;
        }

        public virtual void Update(GameTime gameTime, Vector2 playerPosition) 
        {
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
            _spriteBatch.Draw(_currentAnimation.AnimationTexture, new Rectangle((int)_position.X, (int)_position.Y, _currentAnimation.CurrentFrame.Width, _currentAnimation.CurrentFrame.Height), _currentAnimation.CurrentFrame, Color.White, _angle,_origin,_orinetation,0f);
        }
    }
}
