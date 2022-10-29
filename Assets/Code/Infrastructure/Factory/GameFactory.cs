using Code.Infrastructure.AssetManagement;
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
            background.GetComponent<Canvas>().worldCamera = UnityEngine.Camera.current;
        }

        public void CreateHUD() => _assetProvider.Instantiate(AssetsPath.HUDPath);
    }

}