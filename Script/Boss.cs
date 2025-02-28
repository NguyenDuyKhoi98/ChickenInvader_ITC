using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject EggPrefab; // Tạo GameObject để chứ Prefab trứng
    [SerializeField] private int Health = 100;
    private int Score = 1000; // Điểm của con gà
    [SerializeField]
    private AudioSource hutSFX;
    [SerializeField]
    private AudioSource deadSFX;
    [SerializeField]
    private AudioSource layEggSFX;
    [SerializeField]
    private GameObject bossExplosionPrefab;
    private bool isAllowToSpawnExplosion = true;

    public static Boss Instance;

    private void Awake()
    {
        Instance = this;
        isAllowToSpawnExplosion=true;
    }

    void Start()
    {
        StartCoroutine(SpawnEgg());
        StartCoroutine(MoveBossToRandomPoint());
    }

    public void PutDamage(int damage)
    {
        if (hutSFX != null && hutSFX.enabled)
        {
            hutSFX.Play();
        }
        if (transform.position.y < 4.0f)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Destroy(gameObject);
                var explosion = Instantiate(bossExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 1f);
            }
            // Time.timeScale = 0; // dừng, 1 là tiếp tục
        }
        
    }
    
    IEnumerator SpawnEgg()
    {
        // Spawn trứng
        while (true)
        {
            if (layEggSFX != null && layEggSFX.enabled)
            {
                layEggSFX.volume = 0.2f;
                layEggSFX.Play();
            }
            // Tạo trứng tại vị trí của boss
            Instantiate(EggPrefab, transform.position, Quaternion.identity);
            // Thời gian đẻ là 0 -> 1s
            yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
        }
    }

    public IEnumerator MoveBossToRandomPoint()
    {
        Vector3 point = getRandomPoint();
        // Tạo điểm ngẫu nhiên trong màn hình
        while (transform.position != point)
        {
            // Di chuyển boss đến điểm ngẫu nhiên
            transform.position = Vector3.MoveTowards(transform.position, point, 0.1f);
            // Chờ một chút trước khi di chuyển lần sau
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        // Di chuyển đến điểm ngẫu nhiên khác
        StartCoroutine(MoveBossToRandomPoint());
    }

    public Vector3 getRandomPoint()
    {
        // Tạo điểm ngẫu nhiên trong màn hình
        Vector3 posRandom = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0.5f, 1f)));
        posRandom.z = 0;
        return posRandom;
    }

    private void OnDestroy()
    {
        if (isAllowToSpawnExplosion)
        {
            Instantiate(bossExplosionPrefab, transform.position, Quaternion.identity);
            if (deadSFX != null && deadSFX.enabled)
            {
                deadSFX.Play();
            }
        }
        
        ScoreController.instance.GetScore(Score);
        GameObject spawner = GameObject.FindGameObjectWithTag("spawner");
        if (spawner != null)
        {
            spawner.GetComponent<ChickenSpawner>().bossDestroyedHandle();
        }
        
    }


    public void notAllowToSpawnExlosion()
    {
        this.isAllowToSpawnExplosion = false;
    }

}
