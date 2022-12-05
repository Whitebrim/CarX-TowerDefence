using System.Collections.Generic;
using UnityEngine;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Data
{
    [CreateAssetMenu(fileName = "Tower Branch", menuName = "Scriptable Object/Tower Branch Config", order = 150)]
    public class TowerBranchConfig : ScriptableObject
    {
        [field: SerializeField] public List<TowerConfig> Branch { get; private set; }
    }
}