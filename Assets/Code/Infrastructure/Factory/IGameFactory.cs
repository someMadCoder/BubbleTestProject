using Code.Infrastructure.Services;

namespace Code.Infrastructure.Factory
{
    public interface IGameFactory: IService
    {
        void CreateBackground();
        void CreateHUD();
        void CreateGun();
        void CreateGrid();
    }
}