using Core.Services.ServiceLocator;
using Core.StateMachine.State;

namespace Core.StateMachine
{
    public interface IStateMachine: IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}