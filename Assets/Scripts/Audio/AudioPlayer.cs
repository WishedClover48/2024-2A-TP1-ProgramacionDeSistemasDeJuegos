using System.Collections;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioClip))]
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private OnFinishAction finishAction;
        private AudioSource _source;
        private AudioPlayerPool audioPlayerPool;
        public AudioSource Source
        {
            get
            {
                _source ??= GetComponent<AudioSource>();
                return _source;
            }
        }
        
        public enum OnFinishAction
        {
            None,
            Destroy,
            Deactivate,
        }

        private void Awake()
        {
            audioPlayerPool = ServiceLocator.Instance.GetService<AudioPlayerPool>();

        }

        public void Play(AudioClipData data)
        {
            Source.loop = data.Loop;
            Source.clip = data.Clip;
            Source.outputAudioMixerGroup = data.Group;
            Source.Play();
            var clipLength = data.Clip.length;
            StartCoroutine(DeactivateIn(clipLength));

        }
        
        private IEnumerator DeactivateIn(float seconds)
        {
            yield return new WaitForSeconds(Mathf.Max(seconds, 0));
            audioPlayerPool.ReturnToPool(this);
            gameObject.SetActive(false);
        }

        public AudioPlayer Clone()
        {
            AudioPlayer clonedObject = Instantiate(this);
            return clonedObject;
        }
    }
}
