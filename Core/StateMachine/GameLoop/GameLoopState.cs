using Core.Services.GameConfigService;
using Core.Services.GameDataService;
using Core.Services.ServiceLocator;
using Core.StateMachine.State;
using UnityEngine;

namespace Core.StateMachine.GameLoop
{
    public class GameLoopState: IState, IPhysicsUpdatableState
    {
        private readonly IAllServices _allService;
        private float _step;

        public GameLoopState(IAllServices allServices)
        {
            _allService = allServices;
        }
        
        public void Exit()
        {
            
        }

        public void Enter()
        {
            _step = CalculateStep();
        }

        public void UpdatePhysics()
        {
            //_step;
        }

        private float CalculateStep()
        {
            float framesCount = Application.targetFrameRate / Time.fixedDeltaTime;
            float step = 1f / framesCount;
            return step;
        }
    }
}