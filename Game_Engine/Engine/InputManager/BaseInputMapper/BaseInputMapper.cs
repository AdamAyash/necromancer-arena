using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Game_Engine.Engine
{
    public class BaseInputMapper
    {
        public virtual IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState keyboardState)
        {
            return new List<BaseInputCommand>();
        }

        public virtual IEnumerable<BaseInputCommand> GetMouseState(MouseState keyboardState)
        {
            return new List<BaseInputCommand>();
        }
    }
}
