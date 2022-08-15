using Game_Engine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game_Engine.States
{
    public class GameplayInputCommands : BaseInputCommand
    {
        public class PlayerMoveLeftCommand : GameplayInputCommands { }
        public class PlayerMoveRightCommand : GameplayInputCommands { }
        public class PlayerMoveUpCommand : GameplayInputCommands { }
        public class PlayerMoveDownCommand : GameplayInputCommands { }

        public class PlayerShootCommand : GameplayInputCommands
        {
            public Vector2 MousePosition { get; set; }
            public PlayerShootCommand(MouseState mouseState)
            {
                MousePosition = new Vector2(mouseState.X, mouseState.Y);
            }
        }
    }
}
