using Game_Engine.Engine;

namespace Game_Engine.States
{
    public class GameplayInputCommands : BaseInputCommand
    {
        public class PlayerMoveLeftCommand : GameplayInputCommands { }
        public class PlayerMoveRightCommand : GameplayInputCommands { }
        public class PlayerMoveUpCommand : GameplayInputCommands { }
        public class PlayerMoveDownCommand : GameplayInputCommands { }
    }
}
