using System;
using Microsoft.Xna.Framework;
using Game_Engine.Engine.States;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Game_Engine.Engine.GameObjects.Collision_Detection;

namespace Game_Engine.Engine.GameObjects
{
    public class BaseGameObject
    {
        protected Vector2 _position;
        protected Vector2 _direction;
        protected Vector2 _origin;

        protected Texture2D _objectTexture;
        private Texture2D _boundingBoxTexture;

        protected List<BoundingBox> _boundingBoxes = new List<BoundingBox>();
        protected List<CircleColider> _circleColliders = new List<CircleColider>();

        protected float _scale;
        protected float _angle;

        public int zIndex { get; set; }

        public event EventHandler<BaseGameStateEvent> OnObjectChnaged;

        public List<BoundingBox> BoundingBoxes
        {
            get
            {
                return _boundingBoxes;
            }
        }

        public List<CircleColider> CircleColiders
        {
            get
            {
                return _circleColliders;
            }
        }

        public virtual Vector2 Position
        {
            get
            {
                return this._position;
            }
            set
            {
                var deltaX = value.X - _position.X;
                var deltaY = value.Y - _position.Y;

                foreach (var bb in _boundingBoxes)
                {
                    bb.Position = new Vector2(_position.X + deltaX, bb.Position.Y + deltaY);
                }

                foreach (var cc in _circleColliders)
                {
                    cc.Center = new Vector2((_position.X + deltaX), (Position.Y + deltaY));
                }
                _position = value;

            }
        }

        private void CreateBoundingBoxTexture(GraphicsDevice GraphicsDevice)
        {
            _boundingBoxTexture = new Texture2D(GraphicsDevice,1,1);
            _boundingBoxTexture.SetData<Color>(new Color[] { Color.White });
        }

        public void RenderBoundingBoxes(SpriteBatch _spriteBatch)
        {
            if (_boundingBoxTexture == null)
            {
                CreateBoundingBoxTexture(_spriteBatch.GraphicsDevice);
            }
            foreach (var bb in _boundingBoxes)
            {
                _spriteBatch.Draw(_boundingBoxTexture, bb.Rectangle, Color.Red);
            }
        }

        public void AddBoundingBoxes(BoundingBox bb)
        {
            _boundingBoxes.Add(bb);
        }

        public void AddCircleCollider(CircleColider cc)
        {
            _circleColliders.Add(cc);
        }

        public virtual void Update(GameTime gameTime) { }

        public virtual void OnNotify(BaseGameStateEvent gameEvent) { }
        protected Vector2 CalculateDirection(float offsetAngle = 0.0f)
        {
            _direction = new Vector2((int)Math.Cos(_angle - offsetAngle), (int)Math.Sin(_angle - offsetAngle));
            _direction.Normalize();

            return _direction;
        }
        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_objectTexture, new Rectangle((int)_position.X, (int)_position.Y, _objectTexture.Width, _objectTexture.Height), null, Color.White, _angle, _origin, SpriteEffects.None, 0f);
        }

    }
}
