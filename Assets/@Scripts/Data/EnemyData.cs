using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Object/Enemy Data", order = 100)]
public class EnemyData : ScriptableObject
{
    public string PrefabPath;
    public int Level;
}