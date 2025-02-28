using UnityEngine;
using System.Collections;

public class EggCrack : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb2D; // rigidbody 2D của trứng
    [SerializeField] private Animator _animator; // animator của trứng
    [SerializeField] private AudioSource eggBrokenSFX;

    void Start()
    {
        // Bắt đầu kiểm tra vị trí ngay sau khi tạo trứng
        StartCoroutine(CheckSpawnPosition()); // check vị trí liên tục
    }

    IEnumerator CheckSpawnPosition()
    {
        while (true)
        {
            // Tính toán vị trí của trứng so với màn hình
            Vector3 viewPort = Camera.main.WorldToViewportPoint(transform.position);

            // Nếu vị trí của trứng thấp hơn 0.05 trên màn hình
            if (viewPort.y < 0.05)
            {
                // Lấy Component Collider2D từ GameObject
                CapsuleCollider2D collider = gameObject.GetComponent<CapsuleCollider2D>();

                // Kiểm tra nếu Collider không null thì disable nó
                if (collider != null)
                {
                    collider.enabled = false; // Vô hiệu hóa Collider
                }
                else
                {
                    Debug.Log("Không thể bỏ collider");
                }

                if (eggBrokenSFX != null && eggBrokenSFX.enabled)
                {
                    eggBrokenSFX.volume = 1.0f;
                    eggBrokenSFX.Play();
                }
                // Trứng bị vỡ (Anim -> Chick -> Egg) cơ chế giống với di chuyển ở bên game con chóa trên lớp
                _animator.SetTrigger("Break");

                _rb2D.bodyType = RigidbodyType2D.Static; // ?
                // phá hủy trứng sau 1 giây sau khi chạm y = 0.05
                Destroy(gameObject, 1);
                break;
            }
            yield return null;
        }
    }
}
