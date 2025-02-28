using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float lastFireTime ;
    [SerializeField] private GameObject VFXExplosion;
    [SerializeField] private GameObject Shield;
    private int ScoreOfChickenLeg = 50;
    [SerializeField]
    private int currentTier = 1;
    private bool isAbleToMove = true;
    private bool isAbleToFire = true;
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject leftGun;
    [SerializeField]
    private GameObject rightGun;
    [SerializeField]
    private GameObject centralGun;

    private bool limitMovingArea = true;

    Lasersfx_script lasersfx_Script;
    EatingSFX_script bitesfx_Script;

    [SerializeField]
    private AudioSource deadSfx;
    [SerializeField]
    private AudioSource getPowerupSfx;


    private void Awake()
    {
        limitMovingArea = true;
        Speed = 10.0f;
        lasersfx_Script = GameObject.FindGameObjectWithTag("lasersfx").GetComponent<Lasersfx_script>();
        bitesfx_Script = GameObject.FindGameObjectWithTag("bitesfx").GetComponent<EatingSFX_script>();
        GameObject shipCon = GameObject.FindGameObjectWithTag("shipController");
        if (shipCon != null)
        {
            int tier = shipCon.GetComponent<ShipController>().getCurrentWeaponTier();
            if (tier > 0)
            {
                Debug.Log("Khôi phục lại tier sau khi player chết");
                Debug.Log("tier cũ: " + tier);
                if (tier - 1 > 0)
                    this.currentTier = tier - 1;
                else this.currentTier = 1;
                shipCon.GetComponent<ShipController>().saveWeaponTier(-1);
            }
            else
            {
                Debug.Log("Gán tier cho player từ player info");
                this.currentTier = PlayerInfo.weaponTier;
            }
        }
        else
        {
            Debug.Log("Ko tìm thấy shipController để xét weapon tier");
        }

        // this.currentTier = PlayerInfo.weaponTier;
        Debug.Log("tên scene: "+SceneManager.GetActiveScene().name);
        Debug.Log("stt scene: " + SceneManager.GetActiveScene().buildIndex);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // gọi IEnumerator Disable khiên sau 5 giây
        StartCoroutine(DisableShield(5));

    }

    // Update is called once per frame
    void Update()
    {
        if (isAbleToMove)
        {
            Move();
        }

        if (isAbleToFire) {
            Fire();
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, y, 0);
        transform.position += direction.normalized * Time.deltaTime * Speed;

        // Giới hạn tọa độ X và Y cho phi thuyền không cho ra ngoài camera
        Vector3 TopLeftPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        if (limitMovingArea)
        {
            // Clamp giúp giữ cho tọa độ X và Y nằm trong khoảng cho phép
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, TopLeftPoint.x * -1, TopLeftPoint.x), Mathf.Clamp(transform.position.y, TopLeftPoint.y * -1, TopLeftPoint.y));
        }
    }

    public void movingForward()
    {
        Shield.SetActive(true);
        StartCoroutine(DisableShield(8));
        this.limitMovingArea = false;
        this.allowToMove(false);
        PlayerInfo.savePlayerInfo();
        StartCoroutine(MoveShipToPoint(new Vector3(transform.position.x, 8.0f, 0f)));
        StartCoroutine(loadNextScene());
    }

    IEnumerator MoveShipToPoint(Vector3 point)
    {
        yield return new WaitForSeconds(3.0f);
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, Speed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator loadNextScene()
    {
        yield return new WaitForSeconds(5);
        int nextscene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextscene <= 5)
            SceneManager.LoadScene(nextscene);
        else
        {
            PlayerInfo.resetPlayerInfo();
            SceneManager.LoadScene(7);
        }
    }
    void Fire()
    {
        if (Input.GetMouseButton(0) && Time.time > lastFireTime + 0.3f) 
        {
            instantiateBullet();
            lastFireTime = Time.time;
        }
    }

    void instantiateBullet()
    {

        Vector3 vt1 = centralGun.transform.position + new Vector3(0.06f, 0, 0);
        Vector3 vt2 = centralGun.transform.position + new Vector3(-0.06f, 0, 0);

        switch (currentTier)
        {
            case 1:
                Instantiate(bullet, centralGun.transform.position, Quaternion.Euler(Vector3.zero));
                break;

            case 2:
                Instantiate(bullet, leftGun.transform.position, Quaternion.Euler(Vector3.zero));
                Instantiate(bullet, rightGun.transform.position, Quaternion.Euler(Vector3.zero));
                break;

            case 3:
                Instantiate(bullet, leftGun.transform.position, Quaternion.Euler(Vector3.zero));
                Instantiate(bullet, rightGun.transform.position, Quaternion.Euler(Vector3.zero));
                Instantiate(bullet, centralGun.transform.position, Quaternion.Euler(Vector3.zero));
                break;

            case 4:
                Instantiate(bullet, leftGun.transform.position, Quaternion.Euler(Vector3.zero));
                Instantiate(bullet, rightGun.transform.position, Quaternion.Euler(Vector3.zero));
                Instantiate(bullet, vt1 , Quaternion.Euler(Vector3.zero));
                Instantiate(bullet, vt2 , Quaternion.Euler(Vector3.zero));
                break;

            case 5:
                Instantiate(bullet, leftGun.transform.position, Quaternion.Euler(new Vector3(0, 0, 7f)));
                Instantiate(bullet, rightGun.transform.position, Quaternion.Euler(new Vector3(0, 0, -7f)));
                Instantiate(bullet, centralGun.transform.position, Quaternion.Euler(Vector3.zero));
                Instantiate(bullet, leftGun.transform.position + new Vector3(-0.06f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 15f)));
                Instantiate(bullet, rightGun.transform.position + new Vector3(0.06f, 0, 0), Quaternion.Euler(new Vector3(0, 0, -15f)));
                break;

            case 6:

                Instantiate(bullet, leftGun.transform.position, Quaternion.Euler(new Vector3(0, 0, 7f)));
                Instantiate(bullet, rightGun.transform.position, Quaternion.Euler(new Vector3(0, 0, -7f)));
                Instantiate(bullet, vt1, Quaternion.Euler(Vector3.zero));
                Instantiate(bullet, vt2, Quaternion.Euler(Vector3.zero));
                Instantiate(bullet, leftGun.transform.position + new Vector3(-0.06f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 15f)));
                Instantiate(bullet, rightGun.transform.position + new Vector3(0.06f, 0, 0), Quaternion.Euler(new Vector3(0, 0, -15f)));
                break;

            case 7:
                Instantiate(bullet, leftGun.transform.position, Quaternion.Euler(new Vector3(0, 0, 7f)));
                Instantiate(bullet, rightGun.transform.position, Quaternion.Euler(new Vector3(0, 0, -7f)));
                Instantiate(bullet, centralGun.transform.position, Quaternion.Euler(Vector3.zero));
                Instantiate(bullet, leftGun.transform.position + new Vector3(-0.06f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 15f)));
                Instantiate(bullet, leftGun.transform.position + new Vector3(-0.12f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 18f)));
                Instantiate(bullet, rightGun.transform.position + new Vector3(0.06f, 0, 0), Quaternion.Euler(new Vector3(0, 0, -15f)));
                Instantiate(bullet, rightGun.transform.position + new Vector3(0.12f, 0, 0), Quaternion.Euler(new Vector3(0, 0, -18f)));
                break;

            case 8:
                Instantiate(bullet, leftGun.transform.position, Quaternion.Euler(new Vector3(0, 0, 11f)));
                Instantiate(bullet, rightGun.transform.position, Quaternion.Euler(new Vector3(0, 0, -11f)));
                Instantiate(bullet, vt1, Quaternion.Euler(Vector3.zero));
                Instantiate(bullet, vt2, Quaternion.Euler(Vector3.zero));
                Instantiate(bullet, leftGun.transform.position + new Vector3(-0.06f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 15f)));
                Instantiate(bullet, leftGun.transform.position + new Vector3(0.06f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 7f)));
                Instantiate(bullet, rightGun.transform.position + new Vector3(0.06f, 0, 0), Quaternion.Euler(new Vector3(0, 0, -15f)));
                Instantiate(bullet, rightGun.transform.position + new Vector3(-0.06f, 0, 0), Quaternion.Euler(new Vector3(0, 0, -7f)));
                break;

            case 9:
                Instantiate(bullet, leftGun.transform.position, Quaternion.Euler(new Vector3(0, 0, 11f)));
                Instantiate(bullet, rightGun.transform.position, Quaternion.Euler(new Vector3(0, 0, -11f)));
                Instantiate(bullet, centralGun.transform.position, Quaternion.Euler(Vector3.zero));
                Instantiate(bullet, vt1, Quaternion.Euler(new Vector3(0, 0, -3f)));
                Instantiate(bullet, vt2, Quaternion.Euler(new Vector3(0, 0, 3f)));
                Instantiate(bullet, leftGun.transform.position + new Vector3(-0.06f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 15f)));
                Instantiate(bullet, leftGun.transform.position + new Vector3(0.06f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 7f)));
                Instantiate(bullet, rightGun.transform.position + new Vector3(0.06f, 0, 0), Quaternion.Euler(new Vector3(0, 0, -15f)));
                Instantiate(bullet, rightGun.transform.position + new Vector3(-0.06f, 0, 0), Quaternion.Euler(new Vector3(0, 0, -7f)));
                break;

            case 10:
                Instantiate(bullet, leftGun.transform.position, Quaternion.Euler(new Vector3(0, 0, 11f)));
                Instantiate(bullet, rightGun.transform.position, Quaternion.Euler(new Vector3(0, 0, -11f)));
                Instantiate(bullet, vt1, Quaternion.Euler(Vector3.zero));
                Instantiate(bullet, vt2, Quaternion.Euler(Vector3.zero));
                Instantiate(bullet, vt1, Quaternion.Euler(new Vector3(0, 0, -3f)));
                Instantiate(bullet, vt2, Quaternion.Euler(new Vector3(0, 0, 3f)));
                Instantiate(bullet, leftGun.transform.position + new Vector3(-0.06f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 15f)));
                Instantiate(bullet, leftGun.transform.position + new Vector3(0.06f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 7f)));
                Instantiate(bullet, rightGun.transform.position + new Vector3(0.06f, 0, 0), Quaternion.Euler(new Vector3(0, 0, -15f)));
                Instantiate(bullet, rightGun.transform.position + new Vector3(-0.06f, 0, 0), Quaternion.Euler(new Vector3(0, 0, -7f)));
                break;

            default:
                break;

        }

        lasersfx_Script.playLasersfx();
    }

    IEnumerator DisableShield(int time)
    {
        yield return new WaitForSeconds(time);
        Shield.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Egg") || collision.CompareTag("Chicken"))
        {
            if (collision.CompareTag("Egg"))
                Destroy(collision.gameObject);
            if (!Shield.activeSelf)
            {
                Destroy(gameObject);
            }
                
        }
        else if (collision.CompareTag("ChickenLeg"))
        {
            Destroy(collision.gameObject);
            bitesfx_Script.playBite();
            ScoreController.instance.GetScore(ScoreOfChickenLeg);
        }
        else if (collision.CompareTag("Boss"))
        {
            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("powerup"))
        {
            Debug.Log("Nhận được powerup");
            getPowerupSfx.Play();
            if(this.currentTier <10)
                upgradeWeapon();
            Destroy(collision.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            if (deadSfx != null && deadSfx.enabled)
            {
                deadSfx.Play();
            }
            var explosion = Instantiate(VFXExplosion, transform.position, Quaternion.identity);
            ShipController.Instance.saveWeaponTier(currentTier);
            Destroy(explosion, 1f);
            GameObject.FindGameObjectWithTag("playerDeadMusic").GetComponent<PlayerDeadMusic_script>().playPlayerDeadMusic();
            ShipController.Instance.SpamShip();
            
        }
    }

    public int getBulletDamage()
    {
        return 5;
    }

    public void allowToMove(bool permit)
    {
        this.isAbleToMove = permit;
    }

    void upgradeWeapon()
    {
        this.currentTier++;
    }

    public int getWeaponTier()
    {
        return this.currentTier;
    }
    
   

}
