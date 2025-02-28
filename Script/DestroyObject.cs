using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private float DistanceDestroy = 17; 
    
    void Update()
    {
        DestroyObjectOverScreen();
    }

    void DestroyObjectOverScreen()
    {
        // Tính toán vị trí trung tâm màn hình
        Vector3 CenterScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        // Nếu vị trí của vật thể ở ngoài màn hình thì destroy  
        if (Vector3.Distance(transform.position, CenterScreen) > DistanceDestroy)
        {
            Destroy(this.gameObject);
        }
    }

}
