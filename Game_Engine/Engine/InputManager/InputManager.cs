using System;
using Microsoft.Xna.Framework;
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

        public float MouseX
        {
            get
            {
                return mouseState.X;
            }
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
        public void Update()
        {
            mouseState = Mouse.GetState();
        }

        public void GetMouseCommands(Action<BaseInputCommand> commandAction)
        {
            _commands = _mapper.GetMouseState(mouseState);
            foreach (var command in _commands)
            {
                commandAction(command);
            }
        }
    }
}
