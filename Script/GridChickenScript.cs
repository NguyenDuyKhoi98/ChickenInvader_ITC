using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GridChickenScript : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    public static GridChickenScript instance;
    public bool isBeingDestroy = false;

    private void Awake()
    {
        instance = this;
        animator.SetBool("finnishWave1", false);
    }

    private void Update()
    {
        bool kq = animator.GetBool("finnishWave1");
        // Debug.Log("kq: "+ kq);
        if (transform.position.y <= 1)
        {
            animator.SetBool("finnishWave1", false);
        }

    }

    public void isComingToStage(bool x)
    {
        
        if (x) {
            transform.position = new Vector3(0, 0, 0);
            animator.StopPlayback();
            animator.SetBool("finnishWave1", x);
            animator.Play("Coming to State");
        } 
        else animator.SetBool("finnishWave1", x);
        Debug.Log("Vị trí x của grid: "+transform.position.x);
        
    }
    public Vector3 currentPosition()
    {
        return transform.position;
    }

    private void OnDestroy()
    {
        Debug.Log("Phá hủy grid chicken và object khác");
        isBeingDestroy = true;
    }
}
