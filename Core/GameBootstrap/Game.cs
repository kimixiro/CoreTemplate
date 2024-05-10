using Core.Services.ServiceLocator;
using Core.StateMachine.GameLoop;
using UnityEngine;

namespace Core.GameBootstrap
{
    public class Game : MonoBehaviour
    {
            [SerializeField] private Transform _npcContainer;

            private GameStateMachine _stateMachine;
        
            private void Awake()
            {
                InitializeStateMachine();
            }

            private void InitializeStateMachine()
            {
                if (_npcContainer == null)
                {
                    Debug.LogError("NPC Container is not assigned in the inspector.");
                    return;
                }
            
                _stateMachine = new GameStateMachine(AllServices.Container, _npcContainer);
                _stateMachine.Enter<BootstrapState>();
            }

            private void FixedUpdate()
            {
                _stateMachine?.UpdateStatePhysics();
            }

            private void Update()
            {
                _stateMachine?.UpdateStateLogic();
            }
    }
}