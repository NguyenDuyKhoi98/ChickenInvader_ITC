using UnityEngine;

public class PlayerDeadMusic_script : MonoBehaviour
{
    [SerializeField]
    private AudioSource playerDeadSF;

    public void playPlayerDeadMusic()
    {
        playerDeadSF.Play();
    }
}
