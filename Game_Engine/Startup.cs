using System;
using Game_Engine.States;

namespace Game_Engine
{
    public static class Startup
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Core(new SplashState()))
                game.Run();
        }
    }
}
