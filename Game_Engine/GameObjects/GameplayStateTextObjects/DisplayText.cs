using System;
using System.Text;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.GameObjects.GameplayStateTextObjects
{
    public class DisplayText : BaseGameText
    {
        private float _opacity;
        private float _opacityFadingRate;
        public DisplayText(SpriteFont font, int waveIndex, Vector2 position) : base(font)
        {
            Text = "Wave " + waveIndex;
            Position = position;
            _opacity = 40;
            _opacityFadingRate = 0.1f;


        }
        public override void Update(GameTime gameTime)
        {
            _opacity -= _opacityFadingRate;
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(Font, Text, _position, Color.White * _opacity);
        }
    }
}
