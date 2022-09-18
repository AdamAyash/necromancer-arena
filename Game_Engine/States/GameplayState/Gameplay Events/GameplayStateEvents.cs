using Game_Engine.Engine.States;

namespace Game_Engine.States
{
    public class GameplayStateEvents : BaseGameStateEvent
    {
        public class SpawnDemonEnemy : GameplayStateEvents { }

        public class SpawnOldWizard : GameplayStateEvents { }

        public class SpawnZombie : GameplayStateEvents { }

        public class SpawnDemonBoss : GameplayStateEvents { }

        public class EntityHit : GameplayStateEvents { }

        public class BossFight : GameplayStateEvents { }

        public class PlayerShoots : GameplayStateEvents { }
    }
}
