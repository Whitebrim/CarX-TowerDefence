using UnityEngine;
using UnityEngine.AddressableAssets;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Data
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Scriptable Object/Projectile Config", order = 102)]
    public class ProjectileConfig : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject Prefab { get; private set; }
        [SerializeField] private ProjectileData data;
        public ProjectileData Data => data;
    }
}