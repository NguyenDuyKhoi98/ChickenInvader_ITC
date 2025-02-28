using System.Collections;
using UnityEngine;

public class DestroyAnimation : MonoBehaviour
{
    public Animator animator;
    public float animationLength = 1.0f; // Thay đổi giá trị này theo thời lượng animation của bạn

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animationLength);
        Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(DestroyAfterAnimation());
    }
}
