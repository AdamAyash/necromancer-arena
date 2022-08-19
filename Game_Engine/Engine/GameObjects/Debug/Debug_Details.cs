using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.Engine.GameObjects.Debug
{
    public class Debug_Details : BaseGameText
    {
        public float FPS { get; set; }

        public Debug_Details(SpriteFont font) : base(font)
        {
            _position = new Vector2(0, 0);
        }

        public override void Update(GameTime gameTime)
        {
            FPS = 1f / (float)gameTime.ElapsedGameTime.TotalSeconds;

            Text = $"FPS: {FPS}";
        }
    }
}
