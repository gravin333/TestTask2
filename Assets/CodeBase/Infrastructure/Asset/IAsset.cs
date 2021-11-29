using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Asset
{
    public interface IAsset : IService
    {
        Task<GameObject> Instantiate(string assetPath);
        Task<GameObject> Instantiate(string assetPath, Transform parent);
    }
}