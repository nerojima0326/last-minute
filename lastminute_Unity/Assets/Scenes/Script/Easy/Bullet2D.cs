using UnityEngine;

public class Bullet2D : MonoBehaviour
{
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 dir, float speed)
    {
        if (rb != null)
            // ★ 유니티 6 버전의 새로운 물리 속도 규격인 linearVelocity를 적용합니다.
            rb.linearVelocity = dir * speed;

        // ==========================================================
        // [★추가] 총알이 날아가는 방향(dir)을 바라보도록 각도를 조절합니다.
        // 이 코드는 총알 원본 그림이 오른쪽(가로)을 보고 있을 때 완벽하게 작동합니다.
        // ==========================================================
        if (dir != Vector2.zero)
        {
            transform.right = dir;
        }
        // ==========================================================
    }

    // [기능 1] 좀비와 부딪히면 둘 다 소멸
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // 좀비 삭제
            Destroy(gameObject);           // 총알 삭제
        }
    }

    // [기능 2] 맞지 않고 카메라 화면 끝(밖)으로 나가면 자동 소멸
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}