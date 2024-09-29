using Audio;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Enemy))]
    public class EnemySfx : MonoBehaviour
    {
        [SerializeField] private AudioPlayer audioSourcePrefab;
        [SerializeField] private SFX_SO spawnClips;
        [SerializeField] private SFX_SO explosionClips;
        private Enemy _enemy;

        private void Reset() => FetchComponents();

        private void Awake() => FetchComponents();
    
        private void FetchComponents()
        {
            // "a ??= b" is equivalent to "if(a == null) a = b"
            _enemy ??= GetComponent<Enemy>();
        }
        
        private void OnEnable()
        {
            if (!audioSourcePrefab)
            {
                Debug.LogError($"{nameof(audioSourcePrefab)} is null!");
                return;
            }
            _enemy.OnSpawn += HandleSpawn;
            _enemy.OnDeath += HandleDeath;
        }
        
        private void OnDisable()
        {
            _enemy.OnSpawn -= HandleSpawn;
            _enemy.OnDeath -= HandleDeath;
        }

        private void HandleDeath()
        {
            PlayRandomClip(explosionClips._soundEffects, audioSourcePrefab);
        }

        private void HandleSpawn()
        {
            PlayRandomClip(spawnClips._soundEffects, audioSourcePrefab);
        }

        private void PlayRandomClip(RandomContainer<AudioClipData> container, AudioPlayer sourcePrefab)
        {
            if (!container.TryGetRandom(out var clipData))
                return;
            
            SpawnSource(sourcePrefab).Play(clipData);
        }

        private AudioPlayer SpawnSource(AudioPlayer prefab)
        {
            return audioSourcePrefab.Clone(transform.position, transform.rotation) as AudioPlayer;
        }

    }
}
