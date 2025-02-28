using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject exitDialog;

    public void newGame()
    {
        PlayerInfo.resetPlayerInfo();
        SceneManager.LoadScene(6);
    }
    public void openOrCloseExitDialog()
    {

        exitDialog.SetActive(!exitDialog.activeSelf);

    }
    public void closeExitDialog()
    {

        exitDialog.SetActive(false);

    } 

    public void exitGame()
    {
        Application.Quit();
    }

}
