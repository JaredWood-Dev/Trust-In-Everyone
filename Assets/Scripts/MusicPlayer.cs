using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] playlist;
    private AudioSource _audioSource;
    public int currentSong = 0;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = playlist[0];
    }

    void Update()
    {
        if (!_audioSource.isPlaying)
        {
            currentSong = (currentSong + 1) % playlist.Length;
            _audioSource.clip = playlist[currentSong];
            _audioSource.Play();
        }
    }
}
