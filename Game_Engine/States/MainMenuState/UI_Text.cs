using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game_Engine.States.MainMenuState
{
    public class UI_Text : BaseGameText
    {
        public UI_Text(SpriteFont font, Vector2 position) :base(font)
        {
            _position = position;
        }
    }
}
