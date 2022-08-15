using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Game_Engine.Engine
{
    public class InputManager
    {
        private BaseInputMapper _mapper;
        private IEnumerable<BaseInputCommand> _commands;
        private KeyboardState keyboardState;
        private MouseState mouseState;
        public InputManager(BaseInputMapper mapper)
        {
            _mapper = mapper;
        }

        public void GetKeyboardCommands(Action<BaseInputCommand> commandAction)
        {
            keyboardState = Keyboard.GetState();
            _commands = _mapper.GetKeyboardState(keyboardState);
            foreach (var command in _commands)
            {
                commandAction(command);
            }
        }

        public void GetMouseCommands(Action<BaseInputCommand> commandAction)
        {
            mouseState = Mouse.GetState();
            _commands = _mapper.GetMouseState(mouseState);
            foreach (var command in _commands)
            {
                commandAction(command);
            }
        }
    }
}
