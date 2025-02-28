using UnityEngine;

public class EatingSFX_script : MonoBehaviour
{
    [SerializeField]
    private AudioSource bite;
    public void playBite()
    {
        bite.Play();
    }
}
