namespace Core.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string Bootstrap = "Bootstrap";
        private const string Game = "Game";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(Bootstrap, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() => _stateMachine.Enter<LoadLevelState, string>(Game);

        private void RegisterServices()
        {
        }
    }
}