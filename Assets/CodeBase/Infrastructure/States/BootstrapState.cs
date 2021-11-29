using CodeBase.Infrastructure.Asset;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IBaseState
    {
        private readonly AllServices _allServices;
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine gameStateMachine, AllServices allServices, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _allServices = allServices;

            RegisterServices();
        }

        public void Exit()
        {
        }

        public async void Enter()
        {
            await _sceneLoader.Load(SceneAssetPath.Init, OnLoadedInit);
        }

        private void RegisterServices()
        {
            _allServices.Register<IAsset>(new AssetProvider());
            _allServices.Register<IUIFactory>(new UIFactory(_allServices.Single<IAsset>()));
            _allServices.Register(_gameStateMachine);
        }

        private async void OnLoadedInit()
        {
            await _sceneLoader.Load(SceneAssetPath.Menu, OnLoadedeMenu);
        }

        private void OnLoadedeMenu()
        {
            _gameStateMachine.Enter<LoadGameState>();
        }
    }
}