using System.Collections;
using UnityEngine;

public class BossDefeatedMusic_script : MonoBehaviour
{
    [SerializeField]
    private AudioSource bossDefeatedMusic;
    public void playBossDefeatedMusic()
    {
        StartCoroutine(choiNhac());
    }

    IEnumerator choiNhac()
    {
        BackgroundMusic_script bgm = GameObject.FindGameObjectWithTag("backgroundMusic").GetComponent<BackgroundMusic_script>();
        bgm.pauseBackgroundMusic();
        bossDefeatedMusic.pitch = 2.0f;
        bossDefeatedMusic.Play();
        yield return new WaitForSeconds(6.0f);
        bgm.playBackgroundMusic();
    }
}
