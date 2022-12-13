using Code.BallGridLogic;
using Code.CameraLogic;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Services;
using Code.Services.Input;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        
        public GameFactory(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public void CreateBackground()
        {
            GameObject background = _assetProvider.Instantiate(AssetsPath.BackgroundPath);
            background.GetComponent<Canvas>().worldCamera = MainCamera;
        }

        public void CreateHUD() => _assetProvider.Instantiate(AssetsPath.HUDPath);
        public void CreateGun()
        {
            GameObject gun = _assetProvider.Instantiate(AssetsPath.GunPath);
            gun.GetComponent<Gun.GunController>().Init(MainCamera, AllServices.Container.Single<IInputService>());
        }

        public void CreateGrid()
        {
            GameObject grid = _assetProvider.Instantiate(AssetsPath.GridPath);
            float yPosition = MainCamera.transform.position.y;
            float screenWidth = WorldCameraBorders.Right(MainCamera) - WorldCameraBorders.Left(MainCamera);
            BallGridGenerator gridGenerator = new BallGridGenerator(grid.GetComponent<BallGrid>().Balls, grid.GetComponent<BallGrid>(), yPosition, 30);
            gridGenerator.Generate(screenWidth,
                BallColor.Blue, BallColor.Green, BallColor.Red, BallColor.Purple, BallColor.Yellow);
        }

        private Camera MainCamera => Camera.main ? Camera.main : 
            _assetProvider.Instantiate(AssetsPath.CameraPath).GetComponent<Camera>();

    }

}