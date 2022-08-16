﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.Engine.Animation
{
    public class Animation
    {
        private Rectangle[] _animationFrames;
        private Texture2D _animationTexture;
        private float _timeElapsed;

        public bool isLooping;
        private float _timeToUpdate;

        private int _frameIndex;

        public int FramePerSecond { set { _timeToUpdate = (1f / value); } }
        public Texture2D AnimationTexture
        {
            get
            {
                return _animationTexture;
            }
        }

        public Rectangle CurrentFrame
        {
            get
            {
                return _animationFrames[_frameIndex];
            }
        }


        public Animation(Texture2D texture, int frames, int fps)
        {
            _frameIndex = 0;
            isLooping = true;
            _animationTexture = texture;
            int width = _animationTexture.Width / frames;
            _animationFrames = new Rectangle[frames];
            FramePerSecond = fps;
            for (int i = 0; i < frames; i++)
            {
                _animationFrames[i] = new Rectangle(i * width, 0, width, _animationTexture.Height);
            }
        }

        public void Update(GameTime gameTime)
        {
            _timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timeElapsed > _timeToUpdate)
            {
                _timeElapsed -= _timeToUpdate;
                if (_frameIndex < _animationFrames.Length - 1)
                {
                    _frameIndex++;
                }
                else if (isLooping)
                {
                    _frameIndex = 0;
                }
            }
        }
    }
}
