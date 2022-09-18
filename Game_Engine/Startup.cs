using System;
using Game_Engine.States;
using Game_Engine.States.DevelopmentState;
using Game_Engine.States.MainMenuState;

namespace Game_Engine
{
    public static class Startup
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Core(new MainMenuState()))
                game.Run();
        }
    }
}
