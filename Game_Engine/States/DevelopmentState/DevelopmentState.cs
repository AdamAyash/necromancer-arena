using Game_Engine.Engine.States;
using Game_Engine.GameObjects.Potions;
using Game_Engine.ParticleSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.States.DevelopmentState
{
    public class DevelopmentState : BaseGameState
    {
        private HealthPotion _potion;
        public override void HandleInput(GameTime gameTime)
        {

        }

        public override void LoadContent()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            _potion.Update(gameTime);
        }
    }
}
