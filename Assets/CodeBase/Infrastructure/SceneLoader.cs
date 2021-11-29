using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class SceneLoader
    {
        public async Task Load(string assetPath, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name.Equals(assetPath))
            {
                onLoaded?.Invoke();
                return;
            }

            await Addressables.LoadSceneAsync(assetPath).Task;

            onLoaded?.Invoke();
        }
    }
}