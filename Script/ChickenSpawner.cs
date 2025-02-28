using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChickenSpawner : MonoBehaviour
{
    private float grid = 1f; // Khoảng cách giữa các con gà
    private Vector3 spawnPosition; // Vị trí xuất hiện của con gà
    private int chickenCurrent; // Số lượng con gà hiện có
    [SerializeField] private GameObject chickenPrefab; 
    [SerializeField] private Transform gridChicken; // Tạo Grid để chứa gà
    [SerializeField] private GameObject BossPrefab; // Boss sẽ xuất hiện sau khi hết con gà
    private int wave = 1;
    private float height = 0;
    private float width = 0;
    private int currenStage = 1;
    private int bossesKilled = 0;

    public static ChickenSpawner Instance;

    private void Awake()
    {
        Instance = this; // Đặt Instance = this để truy cập đến ChickenSpawner
        BossPrefab.SetActive(false);

        height = Camera.main.orthographicSize * 2; // Tính toán chiều cao camera
        width = height * Screen.width / Screen.height; // Tính toán chiều rộng camera
        spawnPosition = new Vector3(0, Camera.main.orthographicSize - grid / 2, 0); // Tính toán vị trí xuất hiện của con gà
        currenStage = SceneManager.GetActiveScene().buildIndex;
    }

    void Start()
    {
        if(GameObject.FindGameObjectWithTag("gridChicken").GetComponent<GridChickenScript>().isBeingDestroy == false)
            SpawnChicken(Mathf.RoundToInt(height / 2 / grid), Mathf.RoundToInt(width / grid / 1.5f)); // Spawn con gà
    }

    public void SpawnChicken(int row, int numberOfChicken)
    {
        float startX = -((numberOfChicken - 1) * grid) / 2; // Tính toán vị trí bắt đầu để căn giữa
        float startY = Camera.main.orthographicSize - grid - grid / 2; // Tính toán vị trí bắt đầu để căn giữa
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < numberOfChicken; j++)
            {
                spawnPosition.x = startX + j * grid; // Căn giữa các con gà
                // Tính toán vị trí X để các con gà cách đều nhau
                spawnPosition.y = startY - i * grid;
                // Tính toán vị trí Y để các con gà cách đều nhau
                GameObject chicken = Instantiate(chickenPrefab, spawnPosition, Quaternion.identity);
                // Khởi tạo con gà tại vị trí được tính toán
                chicken.transform.parent = gridChicken;
                // Đặt con gà vào trong GridChicken
                chickenCurrent++;
                // Tăng số lượng con gà hiện có
            }
        }
    }

    public void DecreaseChicken() // count down to spawn boss
    {
        chickenCurrent--;
        if (chickenCurrent == 0)
        {
            switch (currenStage)
            {
                case 1:
                    StartCoroutine(BossSpawning(0));
                    break;
                case 2:
                    EnemySpawning_Stage2();
                    break;
                case 3:
                    EnemySpawning_Stage3();
                    break;
                case 4:
                    EnemySpawning_Stage4();
                    break;
                case 5:
                    EnemySpawning_Stage5();
                    break;
            }
        }
        
    }

    void EnemySpawning_Stage2() // 2 wave quái
    {
        if (wave <2)
        {
            GameObject gc = GameObject.FindGameObjectWithTag("gridChicken");
            if (gc != null)
            {
                if(gc.GetComponent<GridChickenScript>().isBeingDestroy == false)
                {
                    gc.GetComponent<GridChickenScript>().isComingToStage(true);
                    SpawnChicken(Mathf.RoundToInt(height / 2 / grid), Mathf.RoundToInt(width / grid / 1.5f)); // Spawn wave 2
                    wave++;
                }
            }
        }
        else
        {
            StartCoroutine(BossSpawning(0));
        }
    }

    void EnemySpawning_Stage3() // 3 wave quái
    {
        if (wave < 3)
        {
            GameObject gc = GameObject.FindGameObjectWithTag("gridChicken");
            if (gc != null)
            {
                if (gc.GetComponent<GridChickenScript>().isBeingDestroy == false)
                {
                    gc.GetComponent<GridChickenScript>().isComingToStage(true);
                    SpawnChicken(Mathf.RoundToInt(height / 2 / grid), Mathf.RoundToInt(width / grid / 1.5f)); // Spawn wave 2
                    wave++;
                }
            }
        }
        else
        {
            StartCoroutine(BossSpawning(0));
        }
    }

    void EnemySpawning_Stage4() // 3 wave quái, 2 boss
    {
        if (wave < 3)
        {
            GameObject gc = GameObject.FindGameObjectWithTag("gridChicken");
            if (gc != null)
            {
                if (gc.GetComponent<GridChickenScript>().isBeingDestroy == false)
                {
                    gc.GetComponent<GridChickenScript>().isComingToStage(true);
                    SpawnChicken(Mathf.RoundToInt(height / 2 / grid), Mathf.RoundToInt(width / grid / 1.5f)); // Spawn wave 2
                    wave++;
                }
            }
        }
        else
        {
            StartCoroutine(BossSpawning(-2.0f));
            StartCoroutine(BossSpawning(2.0f));
        }
    }

    void EnemySpawning_Stage5() // 3 wave quái
    {
        if (wave < 3)
        {
            GameObject gc = GameObject.FindGameObjectWithTag("gridChicken");
            if (gc != null)
            {
                if (gc.GetComponent<GridChickenScript>().isBeingDestroy == false)
                {
                    gc.GetComponent<GridChickenScript>().isComingToStage(true);
                    SpawnChicken(Mathf.RoundToInt(height / 2 / grid), Mathf.RoundToInt(width / grid / 1.5f)); // Spawn wave 2
                    wave++;
                }
            }
        }
        else
        {
            StartCoroutine(BossSpawning(-3));
            StartCoroutine(BossSpawning(0));
            StartCoroutine(BossSpawning(3));
        }
    }

    IEnumerator BossSpawning(float x)
    {
        yield return new WaitForSeconds(3);
        BossPrefab.SetActive(true);
        BossPrefab = Instantiate(BossPrefab, new Vector3(x, 7.5f, 0f), Quaternion.identity);
        Debug.Log("Boss xuất hiện");
    }

    public void bossDestroyedHandle()
    {
        bossesKilled++;

        switch (currenStage)
        {
            case 1:
            case 2:
            case 3:
                if(bossesKilled == 1)
                {
                    Debug.Log("Đã tiêu diệt boss");
                    GameObject.FindGameObjectWithTag("bossDefeatedMusic").GetComponent<BossDefeatedMusic_script>().playBossDefeatedMusic();
                    GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>().movingForward();
                }
                break;

            case 4:
                if (bossesKilled == 2)
                {
                    Debug.Log("Đã tiêu diệt boss");
                    GameObject.FindGameObjectWithTag("bossDefeatedMusic").GetComponent<BossDefeatedMusic_script>().playBossDefeatedMusic();
                    GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>().movingForward();
                }
                break;

            case 5:
                if (bossesKilled == 3)
                {
                    Debug.Log("Đã tiêu diệt boss");
                    GameObject.FindGameObjectWithTag("bossDefeatedMusic").GetComponent<BossDefeatedMusic_script>().playBossDefeatedMusic();
                    GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>().movingForward();
                }
                break;
        }
    }
    
}
