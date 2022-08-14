using Game_Engine.Engine;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game_Engine.States
{
    public class GameplayInputMapper : BaseInputMapper
    {
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState keyboardState)
        {
            var commands = new List<BaseInputCommand>();

            if (keyboardState.IsKeyDown(Keys.A))
            {
                commands.Add( new GameplayInputCommands.PlayerMoveLeftCommand());
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                commands.Add(new GameplayInputCommands.PlayerMoveRightCommand());
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                commands.Add(new GameplayInputCommands.PlayerMoveUpCommand());
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                commands.Add(new GameplayInputCommands.PlayerMoveDownCommand());
            }
            return commands;
        }
    }
}
