using NTC.Global.Pool;
using UnityEngine;

public class VFXPoolable : MonoBehaviour, IPoolItem
{
    private ParticleSystem _particles;

    private void Start()
    {
        _particles = GetComponent<ParticleSystem>();
    }

    public void OnSpawn()
    {
    }

    public void OnDespawn()
    {
        _particles.Clear();
    }
}
