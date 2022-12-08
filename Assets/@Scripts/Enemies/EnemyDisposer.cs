namespace Enemies
{
    public class EnemyDisposer
    {
        private readonly EnemyFactory _factory;

        public EnemyDisposer(EnemyFactory factory)
        {
            _factory = factory;
        }

        public void OnEnemyKilled(Enemy enemy)
        {
            _factory.Destroy(enemy);
        }

        public void OnEnemyReachedDestination(Enemy enemy)
        {
            _factory.Destroy(enemy);
        }
    }
}