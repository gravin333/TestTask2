using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.MenuWindow
{
    public class MainMenuWindow : MonoBehaviour
    {
        public Button PlayButton;
        private GameStateMachine _gameStateMachine;

        private void OnEnable()
        {
            PlayButton?.onClick.AddListener(PlayGame);
        }

        private void OnDisable()
        {
            PlayButton?.onClick.RemoveListener(PlayGame);
        }

        public void Construct()
        {
            _gameStateMachine = AllServices.Container.Single<GameStateMachine>();
        }

        private void PlayGame()
        {
            _gameStateMachine.Enter<LoadGameState>();
        }
    }
}