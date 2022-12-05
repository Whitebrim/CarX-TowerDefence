using Enemies;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Object/Enemy Data", order = 100)]
public class EnemyConfig : ScriptableObject
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    [field: SerializeField] public AssetReferenceGameObject Prefab { get; private set; }
    [SerializeField] private EnemyData data;
    public EnemyData Data => data;

    private void OnValidate()
    {
        if (data.reachDistance == 0)
        {
            data.reachDistance = EnemyData.DefaultReachDistance;
        }
    }
}