namespace Code.Infrastructure.States
{
    public interface IState: IExitableState
    {
        void Enter();
    }
    public interface IPayLoadedState<TPayLoad>: IExitableState
    {
        void Enter(TPayLoad payLoad);
    }
    public interface IExitableState
    {
        void Exit();
    }
}