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
            background.GetComponent<Canvas>().worldCamera = GetCamera();
        }

        public void CreateHUD() => _assetProvider.Instantiate(AssetsPath.HUDPath);
        public void CreateGun()
        {
            Camera camera = GetCamera();
            GameObject gun = _assetProvider.Instantiate(AssetsPath.GunPath);
            gun.GetComponent<Gun.GunController>().Init(camera, AllServices.Container.Single<IInputService>());
        }

        private Camera GetCamera()
        {
            if(Camera.main!=null)
                return Camera.main;
            else
            {
                GameObject camera = _assetProvider.Instantiate(AssetsPath.CameraPath);
                return camera.GetComponent<Camera>();
            }
        }
    }

}