using Core.Infrastructure.Services;
using Core.Services.AssetManagement;
using Services;
using UnityEngine;

namespace Core.Infrastructure.States
{
    internal class GameLoopState : IState
    {
        private const string SpawnPositionTag = "Spawn Position";
        private const string TargetPositionTag = "Target Position";
        private readonly GameStateMachine _stateMachine;
        private readonly DI _services;
        private readonly IAssetProvider _assetProvider;
        private EnemyController _enemyController;

        public GameLoopState(GameStateMachine stateMachine, DI services, IAssetProvider assetProvider)
        {
            _stateMachine = stateMachine;
            _services = services;
            _assetProvider = assetProvider;
        }

        public void Enter()
        {
            CreateEnemyController();
        }

        public void Exit()
        {
        }

        private void CreateEnemyController()
        {
            _enemyController = new EnemyController(_assetProvider,
                GameObject.FindGameObjectWithTag(SpawnPositionTag).transform.position,
                GameObject.FindGameObjectWithTag(TargetPositionTag).transform.position,
                1);
            _services.RegisterSingle(_enemyController);
        }
    }
}