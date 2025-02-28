using UnityEngine;

public class BackgroundMusic_script : MonoBehaviour
{
    public AudioSource musicSource;

    void Start()
    {
        musicSource.loop = true;
        musicSource.Play();
    }

    public void stopBackgroundMusic()
    {
        musicSource.Stop();
    }

    public void pauseBackgroundMusic()
    {
        musicSource.Pause();
    }

    public void playBackgroundMusic()
    {
        musicSource.Play();
    }
}
