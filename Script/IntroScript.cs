using System.Collections;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    [SerializeField] float _scrollSpeed = 12;
    [SerializeField] private GameObject ship;
    private Vector3 scrollDirection = Vector3.up;
    private bool allowToSpawnSampleShip = true;
    private bool sampleShipExist = false;

    void Update()
    {
        this.transform.position += scrollDirection * _scrollSpeed * Time.deltaTime;
        if(transform.position.y >= 2000 && this.allowToSpawnSampleShip ==true && sampleShipExist == false)
        {
            Debug.Log("allow to spawn ship? " + this.allowToSpawnSampleShip);
            Instantiate(ship, new Vector3(0, -8, 0), Quaternion.Euler(Vector3.zero));
            sampleShipExist = true;
        }
            
    }

    public void cancelSpawningShip()
    {
        this.allowToSpawnSampleShip=false;
    }
}
