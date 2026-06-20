using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float speed = 2.0f; // 좀비 속도
    private Transform playerTransform;

    void Start()
    {
        // "Player" 태그를 가진 오브젝트를 찾아서 타겟으로 잡습니다.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        // 플레이어 방향으로 이동
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }
}