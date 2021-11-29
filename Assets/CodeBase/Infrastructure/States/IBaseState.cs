namespace CodeBase.Infrastructure.States
{
    public interface IBaseState : IExitableState
    {
        void Enter();
    }
}