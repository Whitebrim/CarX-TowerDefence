using Core.Infrastructure.Services;
using Core.Services;
using Core.Services.AssetManagement;

namespace Core.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Bootstrap = "Bootstrap";
        private const string Game = "Game";
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly DI _services;

        public BootstrapState(GameStateMachine stateMachine, DI services)
        {
            _stateMachine = stateMachine;
            _services = services;

            RegisterServices();
            _sceneLoader = _services.Single<ISceneLoader>();
        }

        public void Enter()
        {
            _sceneLoader.Load(Bootstrap, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() => _stateMachine.Enter<LoadLevelState, string>(Game);

        private void RegisterServices()
        {
            _services.RegisterSingle<ISceneLoader>(new SceneLoader(_services.Single<ICoroutineRunner>()));
            _services.RegisterSingle<IAssetProvider>(new AddressablesProvider());
        }
    }
}