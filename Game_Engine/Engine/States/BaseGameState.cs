﻿using System;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.Engine.States
{
    public abstract class BaseGameState
    {
        private const string EMPTY_TEXTURE = "Assets/Sprites/Empty";
        private const string EMPTY_FONT = "Assets/Fonts/Base/BaseFont";

        protected List<BaseGameObject> _gameObjectsList;
        private ContentManager _contentManager;
        private InputManager _inputManager;

        protected bool _debugMode = false;
        public void Intitialize(ContentManager contentManager)
        {
            _contentManager = contentManager;
            _gameObjectsList = new List<BaseGameObject>();
        }

        protected InputManager InputManager
        {
            get
            {
                return this._inputManager;
            }
        }
        protected Texture2D LoadTexture(string textureName)
        {
            try
            {
                return _contentManager.Load<Texture2D>(textureName);
            }
            catch (Exception)
            {
                return _contentManager.Load<Texture2D>(EMPTY_TEXTURE);
            }
        }

        protected SpriteFont LoadSpriteFont(string fontName)
        {
            try
            {
                return _contentManager.Load<SpriteFont>(fontName);
            }
            catch (Exception)
            {
                return _contentManager.Load<SpriteFont>(EMPTY_FONT);
            }
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
        public abstract void Update(GameTime gameTime);

        public abstract void HandleCollision();
        protected void  SetInputManager(BaseInputMapper mapper)
        {
            _inputManager = new InputManager(mapper);
        }
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
