using System.Collections;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField]
    private GameObject EggPrefab;
    [SerializeField]
    private GameObject ChickenLegPrefab; 
    [SerializeField]
    private GameObject powerupPrefab; 
    [SerializeField]
    private GameObject mobExplosionPrefab; 
    private int lifepoint = 10;
    [SerializeField]
    private AudioSource chickenAttacked;
    [SerializeField]
    private AudioSource chickenDeadSFX;
    [SerializeField]
    private AudioSource chickenLayEggSFX;

    private bool isAllowToSpawnEgg = true;
    private bool isAllowToSpawnExplosion = true;
    private void Awake()
    {
        StartCoroutine(SpawnEgg()); 
        isAllowToSpawnExplosion = true;
    }

    IEnumerator SpawnEgg()
    {
        while (isAllowToSpawnEgg)
        {
            yield return new WaitForSeconds(Random.Range(5, 20));
            chickenLayEggSFX.volume = 0.2f;
            chickenLayEggSFX.Play();
            Instantiate(EggPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GridChickenScript grid = GameObject.FindGameObjectWithTag("gridChicken").GetComponent<GridChickenScript>();
        float currentY = grid.currentPosition().y;
        if (collision.CompareTag("Bullet") && currentY <=4 )
        {
            this.lifepoint-=5;
            Destroy(collision.gameObject);
            if(this.lifepoint <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                chickenAttacked.Play();
            }
        }
    }



    private void OnDestroy()
    {
        if (isAllowToSpawnExplosion)
        {
            Instantiate(mobExplosionPrefab, transform.position, Quaternion.identity);
            if (chickenDeadSFX != null && chickenDeadSFX.enabled)
            {
                chickenDeadSFX.Play();
            }
        }
        GameObject spawner = GameObject.FindGameObjectWithTag("spawner");
        if (spawner != null)
        {
            ChickenSpawner chickenSpawner = spawner.GetComponent<ChickenSpawner>();
            if (chickenSpawner != null)
            {
                chickenSpawner.DecreaseChicken();
            }
        }
        int random = Random.Range(0, 101);

        GameObject grid = GameObject.FindGameObjectWithTag("gridChicken");

        if (grid)
        {
            if (random <= 30 && grid.GetComponent<GridChickenScript>().isBeingDestroy == false)
            {
                Instantiate(ChickenLegPrefab, transform.position, Quaternion.identity);
            }

            if (random <= 5 && grid.GetComponent<GridChickenScript>().isBeingDestroy == false)
            {
                Instantiate(powerupPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    public void notAllowToSpawnExlosion()
    {
        this.isAllowToSpawnExplosion = false;
    } 

}
