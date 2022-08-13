using System;
using System.Linq;
using System.Collections.Generic;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.Engine.States
{
    public abstract class BaseGameState
    {
        protected List<BaseGameObject> _gameObjectsList;

        private ContentManager _contentManager;
        public void Intitialize(ContentManager contentManager)
        {
            _contentManager = contentManager;
            _gameObjectsList = new List<BaseGameObject>();
        }
        protected Texture2D LoadTexture(string texturename)
        {
            return _contentManager.Load<Texture2D>(texturename);
        }

        public event EventHandler<BaseGameState> OnStateSwitched;

        public void SwitchState(BaseGameState state)
        {
            OnStateSwitched?.Invoke(EventArgs.Empty, state);
        }
        public void UnloadContent()
        {
            _contentManager.Unload();
        }
        public abstract void Update();

        public abstract void HandleCollision();
        public abstract void LoadContent();
        protected void AddGameObject(BaseGameObject gameObject)
        {
            _gameObjectsList.Add(gameObject);
        }

        protected void RemoveGameObject(BaseGameObject gameObject)
        {
            _gameObjectsList.Remove(gameObject);
        }

        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            _gameObjectsList.OrderBy(o => o.zIndex);
            foreach (var gameObject in _gameObjectsList)
            {
                gameObject.Draw(_spriteBatch);
            }
        }
    }
}
