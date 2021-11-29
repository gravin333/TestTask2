using System.Threading.Tasks;
using CodeBase.Infrastructure.Asset;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAsset _asset;
        private GameObject _rootCanvas;

        public UIFactory(IAsset asset)
        {
            _asset = asset;
        }

        public async Task CreateRootCanvas()
        {
            _rootCanvas = await _asset.Instantiate(UIAssetPath.UIRootCanvas);
        }

        public async Task<GameObject> CreateMenuWindow()
        {
            var gameObject = await _asset.Instantiate(UIAssetPath.MenuWindow, _rootCanvas.transform);
            return gameObject;
        }
    }
}