using Audio;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Enemy))]
    public class EnemySfx : MonoBehaviour
    {
        [SerializeField] private SFX_SO spawnClips;
        [SerializeField] private SFX_SO explosionClips;
        private Enemy _enemy;
        private AudioPlayer audioPlayer;
        private AudioPlayerPool audioPlayerPool;

        private void Reset() => FetchComponents();

        private void Awake() 
        {
            FetchComponents();
            audioPlayerPool = ServiceLocator.Instance.GetService<AudioPlayerPool>();
        }
    
        private void FetchComponents()
        {
            // "a ??= b" is equivalent to "if(a == null) a = b"
            _enemy ??= GetComponent<Enemy>();
        }
        
        private void OnEnable()
        {
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
            PlayRandomClip(explosionClips._soundEffects);
        }

        private void HandleSpawn()
        {
            PlayRandomClip(spawnClips._soundEffects);
        }

        private void PlayRandomClip(RandomContainer<AudioClipData> container)
        {
            if (!container.TryGetRandom(out var clipData))
                return;
            
            audioPlayer = SpawnSource();
            audioPlayer.Play(clipData);
        }

        private AudioPlayer SpawnSource()
        {
            return audioPlayerPool.GetFromPool();
        }

    }
}
