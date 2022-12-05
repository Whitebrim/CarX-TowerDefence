using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Enemy))]
    public class BasicEnemyMovement : MonoBehaviour
    {
        private Enemy _enemy;

        private void Start()
        {
            _enemy = GetComponent<Enemy>();
        }

        private void Update()
        {
            ReachDestination();
        }

        private void ReachDestination()
        {
            transform.position = Vector3.MoveTowards(transform.position, _enemy.Target, _enemy.Speed * Time.deltaTime);

            _enemy.CheckDestinationReached();
        }
    }
}