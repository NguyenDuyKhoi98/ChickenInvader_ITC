using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD_Canvas_script : MonoBehaviour
{
    [SerializeField]
    private GameObject menuObj;

    private void Awake()
    {
        menuObj.SetActive(false);
    }

    public void openOrCloseMenuStage()
    {
        if (menuObj.activeSelf == false)
        {
            Time.timeScale = 0;
        }
        if (menuObj.activeSelf == true)
        {
            Time.timeScale = 1;
        }
        menuObj.SetActive(!menuObj.activeSelf);
    }

    public void restartThisLevel()
    {
        Time.timeScale = 1;
        turnOffExplosionAnimation();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void backToMainMenu()
    {
        Time.timeScale = 1;
        turnOffExplosionAnimation();
        PlayerInfo.resetPlayerInfo();
        Debug.Log("score sau reset: "+PlayerInfo.playerScore);
        
        SceneManager.LoadScene(0);
    }

    private void turnOffExplosionAnimation()
    {
        GameObject[] chickens = GameObject.FindGameObjectsWithTag("Chicken");
        if (chickens != null && chickens.Length != 0)
        {
            foreach (var item in chickens)
            {
                item.GetComponent<Chicken>().notAllowToSpawnExlosion();
            }
        }
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
        if (bosses != null && bosses.Length != 0)
        {
            foreach (var item in bosses)
            {
                item.GetComponent<Boss>().notAllowToSpawnExlosion();
            }
        }
    }


}
