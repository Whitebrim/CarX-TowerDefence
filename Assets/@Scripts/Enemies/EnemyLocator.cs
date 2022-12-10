using UnityEngine;

namespace Enemies
{
    public class EnemyLocator
    {
        private readonly EnemyFactory _factory;

        public EnemyLocator(EnemyFactory factory)
        {
            _factory = factory;
        }

        public Enemy GetNearestEnemy(Vector3 position)
        {
            if (_factory.EnemyList.Count == 0) return null;

            var minDist = float.PositiveInfinity;
            var index = -1;
            for (var i = 0; i < _factory.EnemyList.Count; i++)
            {
                float distance = Vector3.Distance(_factory.EnemyList[i].Position, position);
                if (distance < minDist)
                {
                    minDist = distance;
                    index = i;
                }
            }

            return _factory.EnemyList[index];
        }
    }
}