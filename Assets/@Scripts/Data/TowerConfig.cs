using UnityEngine;
using UnityEngine.AddressableAssets;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Data
{
    [CreateAssetMenu(fileName = "Tower", menuName = "Scriptable Object/Tower Config", order = 151)]
    public class TowerConfig : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject Prefab { get; private set; }
        [field: SerializeField] public ProjectileConfig Projectile { get; private set; }
        [SerializeField] private TowerData data;
        public TowerData Data => data;
    }
}