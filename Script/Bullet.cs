using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 0;
    [SerializeField] private float Speed = 10;// Tốc độ đạn 

    private void Awake()
    {
        GameObject ship = GameObject.FindGameObjectWithTag("Ship");
        if (ship != null)
            damage = ship.GetComponent<Ship>().getBulletDamage();
        else Destroy(this.gameObject);
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * Speed);// Đạn bắn ra
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boss") {
            Debug.Log("boss trúng đạn");
            GameObject boss = collision.gameObject;
            Boss bossScript = boss.GetComponent<Boss>();
            bossScript.PutDamage(damage);
            Destroy(gameObject);
        }
    }   

    
}


