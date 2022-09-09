using Game_Engine.Engine.States;
using Game_Engine.ParticleSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.States.DevelopmentState
{
    public class DevelopmentState : BaseGameState
    {
        private BloodEmitter _emitter;
        public override void HandleInput(GameTime gameTime)
        {

        }

        public override void LoadContent()
        {
            _emitter = new BloodEmitter(LoadTexture("Assets/Particles/BloodPartciles/1"),new Vector2(600,360));
            AddGameObject(_emitter);
        }

        public override void Update(GameTime gameTime)
        {
            _emitter.Update(gameTime);
        }
    }
}
