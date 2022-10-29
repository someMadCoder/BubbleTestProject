using Code.Infrastructure.Services;
using UnityEngine;

namespace Code.Infrastructure.AssetManagement
{
    public interface IAssetProvider:IService
    {
        GameObject Instantiate(string path);
    }
}