using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip buttonEffect;
    private AudioSource _audioSource;
    public void StartGame()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(buttonEffect);
        SceneManager.LoadScene(1);
    }
}
