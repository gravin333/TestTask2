using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IUIFactory : IService
    {
        Task CreateRootCanvas();
        Task<GameObject> CreateMenuWindow();
    }
}