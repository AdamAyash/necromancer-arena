using Game_Engine.Engine.Audio;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game_Engine.Engine.States
{
    public abstract class BaseGameState
    {
        private const string EMPTY_TEXTURE = "Assets/Sprites/Empty";
        private const string EMPTY_FONT = "Assets/Fonts/Base/BaseFont";

        protected SoundManager _soundManager = new SoundManager();

        protected List<BaseGameObject> _gameObjectsList;
        protected GraphicsDevice _graphicsDevice;
        private ContentManager _contentManager;
        private InputManager _inputManager;

        protected bool _debugMode = false;
        public void Intitialize(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            _contentManager = contentManager;
            _graphicsDevice = graphicsDevice;
            _gameObjectsList = new List<BaseGameObject>();
        }

        protected InputManager InputManager
        {
            get
            {
                return this._inputManager;
            }
        }

        protected TiledMap LoadTiledMap(string tiledMapName)
        {
            return _contentManager.Load<TiledMap>(tiledMapName);
        }
        protected Texture2D LoadTexture(string textureName)
        {
            try
            {
                return _contentManager.Load<Texture2D>(textureName);
            }
            catch (ContentLoadException)
            {
                return _contentManager.Load<Texture2D>(EMPTY_TEXTURE);
            }
            catch (ArgumentNullException)
            {
                return _contentManager.Load<Texture2D>(EMPTY_TEXTURE);
            }
        }

        protected Song LoadSong(string songName)
        {
            return _contentManager.Load<Song>(songName);
        }

        protected SoundEffect LoadSoundEffect(string soudnEffectName)
        {
            return _contentManager.Load<SoundEffect>(soudnEffectName);
        }
        protected SpriteFont LoadSpriteFont(string fontName)
        {
            try
            {
                return _contentManager.Load<SpriteFont>(fontName);
            }
            catch (ContentLoadException)
            {
                return _contentManager.Load<SpriteFont>(EMPTY_FONT);
            }
            catch (ArgumentNullException)
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

        public abstract void HandleInput(GameTime gameTime);
        protected void SetInputManager(BaseInputMapper mapper)
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

        protected void OnNotify(BaseGameStateEvent gameEvent)
        {
            _soundManager.OnNotify(gameEvent);
        }
        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            _gameObjectsList.OrderBy(o => o.zIndex);
            foreach (var gameObject in _gameObjectsList)
            {
                gameObject.Draw(_spriteBatch);
                if (_debugMode)
                {
                    gameObject.RenderBoundingBoxes(_spriteBatch);
                }
            }
        }
    }
}
