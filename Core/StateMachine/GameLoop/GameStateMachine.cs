using Core.Services.ServiceLocator;
using UnityEngine;

namespace Core.StateMachine.GameLoop
{
    public class GameStateMachine: BaseStateMachine
    {
        public GameStateMachine(IAllServices allServices, Transform npcContainer)
        {
            AddState(typeof(BootstrapState), new BootstrapState(this, allServices, npcContainer));
            AddState(typeof(LevelLoadState), new LevelLoadState(this, allServices));
            AddState(typeof(GameLoopState), new GameLoopState(allServices));
        }
    }
}