namespace Core.StateMachine.State
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}