using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    public static int playerScore = 0;
    public static int playerLP = 5;
    public static int weaponTier = 5;

    public static void resetPlayerInfo()
    {
        playerScore = 0;
        playerLP = 5;
        weaponTier = 1;
    }

    public static void savePlayerInfo()
    {
        weaponTier =  GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>().getWeaponTier();
        playerLP = GameObject.FindGameObjectWithTag("lpController").GetComponent<Lifepoint_script>().getPlayerHP();
        playerScore = GameObject.FindGameObjectWithTag("scoreController").GetComponent<ScoreController>().scoreGetter();
    }
}
