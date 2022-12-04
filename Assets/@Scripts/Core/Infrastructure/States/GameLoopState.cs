using Core.Infrastructure.Services;
using Core.Services;
using Core.Services.AssetManagement;
using Data;
using Services;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Infrastructure.States
{
    internal class GameLoopState : IState
    {
        private const string SpawnPositionTag = "Spawn Position";
        private const string TargetPositionTag = "Target Position";
        private readonly GameStateMachine _stateMachine;
        private readonly DI _services;
        private readonly IAssetProvider _assetProvider;
        private EnemySpawner _enemySpawner;
        private GameData _gameData;

        public GameLoopState(GameStateMachine stateMachine, DI services, IAssetProvider assetProvider)
        {
            _stateMachine = stateMachine;
            _services = services;
            _assetProvider = assetProvider;
        }

        public void Enter()
        {
            LoadGameData();
            LoadLevel("Test Level");
        }

        public void Exit()
        {
        }

        private void LoadGameData()
        {
            _gameData = (GameData)_assetProvider.Load<ScriptableObject>(AssetConstantPath.GameDataPath);
        }

        private void LoadLevel(string levelName)
        {
            LevelData level = _gameData?.Levels?.FirstOrDefault((x) => x.name == levelName);
            Assert.IsNotNull(level, $"Level named \"{levelName}\" does not exist in this game config!");

            CreateEnemySpawner(ScriptableObject.Instantiate(level)).StartLevel();
        }

        private EnemySpawner CreateEnemySpawner(LevelData levelData)
        {
            _enemySpawner = new EnemySpawner(
                _assetProvider,
                _services.Single<ICoroutineRunner>(),
                levelData,
                GameObject.FindGameObjectWithTag(SpawnPositionTag).transform.position,
                GameObject.FindGameObjectWithTag(TargetPositionTag).transform.position);

            _services.RegisterSingle(_enemySpawner);
            return _enemySpawner;
        }
    }
}