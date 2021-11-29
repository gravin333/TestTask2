using CodeBase.Infrastructure.Factory;
using CodeBase.MenuWindow;

namespace CodeBase.Infrastructure.States
{
    public class LoadMenuState : IBaseState
    {
        private readonly IUIFactory _uiFactory;
        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;

        public LoadMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }

        public void Exit()
        {
        }

        public async void Enter()
        {
            await _uiFactory.CreateRootCanvas();
            var menuWindow = await _uiFactory.CreateMenuWindow();
            menuWindow.GetComponent<MainMenuWindow>().Construct();
        }
    }
}