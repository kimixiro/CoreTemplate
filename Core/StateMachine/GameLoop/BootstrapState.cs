using Core.Services.GameConfigService;
using Core.Services.GameDataService;
using Core.Services.GameFactory;
using Core.Services.Input;
using Core.Services.ResourceLoader;
using Core.Services.ServiceLocator;
using Core.StateMachine.State;
using UnityEngine;

namespace Core.StateMachine.GameLoop
{
    public class BootstrapState: IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IAllServices _services;
        private readonly Transform _npcContainer;

        public BootstrapState(GameStateMachine gameStateMachine, IAllServices services, Transform npcContainer)
        {
            _stateMachine = gameStateMachine;
            _services = services;
            _npcContainer = npcContainer;
        }
        
        public void Enter()
        {
            RegisterServices();
            _stateMachine.Enter<LevelLoadState>();
        }
        
        public void Exit()
        {
            
        }
        
        private void RegisterServices()
        {
            RegisterInputService();
            RegisterGameDataService();
            RegisterResourceService();
            RegisterConfigService();
            RegisterGameFactory();
        }

        private void RegisterInputService()
        {
            _services.RegisterSingle<IInputService>(new StandaloneInputService());
        }

        private void RegisterGameDataService()
        {
            _services.RegisterSingle<IGameDataService>(new GameDataService());
        }

        private void RegisterConfigService()
        {
            _services.RegisterSingle<IGameConfigService>(new GameConfigService(AllServices.Container.Single<IResourceLoader>()));
        }

        private void RegisterResourceService()
        {
            _services.RegisterSingle<IResourceLoader>(new ResourceLoader());
        }

        private void RegisterGameFactory()
        {
            _services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IResourceLoader>()));
        }
    }
}