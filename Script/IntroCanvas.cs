using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject introObj;
    public void skipIntro()
    {
        
        introObj.SetActive(false);
        introObj.GetComponent<IntroScript>().cancelSpawningShip();

        GameObject shipSample = GameObject.FindGameObjectWithTag("shipSample");
        if (shipSample != null)
        {
            shipSample.SetActive(false);
            shipSample.GetComponent<ShipInTheIntro>().destroyThisObject();
        }
        else Debug.Log("Không tìm thấy ship sapmle");
        SceneManager.LoadScene(1);
    }
}
