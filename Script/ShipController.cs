using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{
    public static ShipController Instance;
    [SerializeField] private GameObject shipPrefab; // Prefab của ship
    private int currentTierWeapon = -1;
    private int currentLP = 0;
    [SerializeField]
    private AudioSource gameoverSFX;

    public void Awake()
    {
        Instance = this; // Đặt instance là this để truy cập đến ShipController
        currentLP = PlayerInfo.playerLP;
    }

    public void saveWeaponTier(int tier)
    {
        Debug.Log("Lưu weapon tier: "+ tier);
        this.currentTierWeapon = tier;
    }
    public int getCurrentWeaponTier()
    {
        return this.currentTierWeapon;
    }

    public void SpamShip()
    {
        currentLP--;        
        Debug.Log("trừ 1 máu, còn: "+currentLP);
        Lifepoint_script.instance.SetLifepoint(currentLP);
        if (currentLP > 0)
        {
            var newShip = Instantiate(shipPrefab, Vector3.zero, Quaternion.identity);
            newShip.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0));
            // Tạo điểm mới dưới màn hình , cách dưới màn hình 10% chiều cao
            var point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height * 0.1f, 0));
            // Đặt tọa độ z = 0 để ship không bị mất
            point.z = 0;
            // Di chuyển ship đến điểm mới
            StartCoroutine(MoveShipToPoint(newShip, point));
        }
        else
        {
            StartCoroutine(toFailScene());
        }
    }

    IEnumerator toFailScene()
    {
        GameObject.FindGameObjectWithTag("backgroundMusic").GetComponent<BackgroundMusic_script>().stopBackgroundMusic();
        if (gameoverSFX != null && gameoverSFX.enabled)
        {
            gameoverSFX.Play();
        }
        yield return new WaitForSeconds(10);
        PlayerInfo.resetPlayerInfo();
        SceneManager.LoadScene(8);
    }

    IEnumerator MoveShipToPoint(GameObject ship, Vector3 point)
    {
        Ship ships_cript = ship.GetComponent<Ship>();
        ships_cript.allowToMove(false);
        float timer = 0;
        while (ship && ship.transform.position != point)
        {
            timer += Time.fixedDeltaTime;
            ship.transform.position = Vector3.Lerp(ship.transform.position, point, timer); 

            // Fixed update là hàm được gọi lại sau mỗi 0.02s
            yield return new WaitForFixedUpdate();
        }
        ships_cript.allowToMove(true);
    }

}

