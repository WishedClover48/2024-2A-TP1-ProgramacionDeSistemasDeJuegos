using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyVfx : MonoBehaviour
    {
        private Enemy _enemy;
        [SerializeField] private VFX_SO deathPrefabs;

        private void Reset() => FetchComponents();

        private void Awake() => FetchComponents();

        private void FetchComponents()
        {
            _enemy = GetComponent<Enemy>();
        }

        private void OnEnable()
        {
            _enemy.OnDeath += HandleDeath;
        }

        private void OnDisable()
        {
            _enemy.OnDeath -= HandleDeath;
        }

        private void HandleDeath()
        {
            if(!deathPrefabs._particleEffects.TryGetRandom(out var prefab))
                return;
            var vfx = Instantiate(prefab, transform.position, transform.rotation);
            var mainModule = vfx.main;
            mainModule.stopAction = ParticleSystemStopAction.Destroy;
        }
    }
}
