using Core.Services.GameFactory;
using Core.Services.Input;
using Core.Services.ServiceLocator;
using UnityEngine;

namespace ShooterKitchen
{
    public class Player : MonoBehaviour
    {
        private IInputService _inputService;
        private IGameFactory _gameFactory;
        
        private void Start()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _gameFactory = AllServices.Container.Single<IGameFactory>();
        }
    }
}