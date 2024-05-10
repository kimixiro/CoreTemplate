using UnityEngine;
using System.Threading.Tasks;
using Core.Services.GameFactory;
using Core.Services.ServiceLocator;
using Core.StateMachine.State;
using ShooterKitchen;


namespace Core.StateMachine.GameLoop
{
    public class LevelLoadState: IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IAllServices _services;

        public LevelLoadState(GameStateMachine gameStateMachine, IAllServices allServices)
        {
            _stateMachine = gameStateMachine;
            _services = allServices;
        }
        
        public async void Enter()
        {
            await InitPlayer();
            _stateMachine.Enter<GameLoopState>();
        }
        
        public void Exit()
        {
            
        }
        

        private async Task InitPlayer()
        {
            IGameFactory gameFactory = _services.Single<IGameFactory>();
            Player player = await gameFactory.CreatePlayer(Vector2.zero);
        }
    }
}