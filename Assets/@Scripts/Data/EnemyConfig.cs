using UnityEngine;
using UnityEngine.AddressableAssets;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Data
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Object/Enemy Config", order = 100)]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject Prefab { get; private set; }
        [SerializeField] private EnemyData data;
        public EnemyData Data => data;

        private void OnValidate()
        {
            if (data.reachDistance == 0)
            {
                data.reachDistance = EnemyData.DefaultReachDistance;
            }

            data.currentHp = data.hp;
        }
    }
}