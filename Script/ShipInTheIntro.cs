using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipInTheIntro : MonoBehaviour
{
    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * 5.0f;
        if (transform.position.y >= 8)
            SceneManager.LoadScene(1);
    }

    public void destroyThisObject()
    {
        Destroy(gameObject);
    }

}
