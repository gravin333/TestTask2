using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Asset
{
    public class AssetProvider : IAsset
    {
        public AssetProvider()
        {
            Initialize();
        }

        public Task<GameObject> Instantiate(string assetPath)
        {
            return Addressables.InstantiateAsync(assetPath).Task;
        }

        public Task<GameObject> Instantiate(string assetPath, Transform parent)
        {
            return Addressables.InstantiateAsync(assetPath, parent).Task;
        }

        private void Initialize()
        {
            Addressables.InitializeAsync();
        }
    }
}