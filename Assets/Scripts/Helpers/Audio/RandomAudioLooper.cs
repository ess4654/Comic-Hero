using UnityEngine;

namespace ComicHero.Helpers.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class RandomAudioLooper : MonoBehaviour
    {
        [SerializeField] AudioClip[] clips;

        private void Start()
        {
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().clip = clips.SelectRandom();
            GetComponent<AudioSource>().Play();
        }
    }
}