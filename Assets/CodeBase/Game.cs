using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;

namespace CodeBase
{
    public class Game
    {
        public GameStateMachine GameStateMachine;

        public Game()
        {
            GameStateMachine = new GameStateMachine(AllServices.Container, new SceneLoader());
        }
    }
}