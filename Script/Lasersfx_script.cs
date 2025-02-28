using UnityEngine;

public class Lasersfx_script : MonoBehaviour
{
    [SerializeField]
    private AudioSource lasersfx;

    public void playLasersfx()
    {
        lasersfx.Play();
    }
}
