using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.States
{
    public class LoadGameState : IBaseState
    {
        private readonly SceneLoader _sceneLoader;
        private GameStateMachine _gameStateMachine;

        public LoadGameState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            _sceneLoader.Load(SceneAssetPath.Game, OnLoaded);
        }

        private void OnLoaded()
        {
        }
    }
}