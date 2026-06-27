using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [Header("난이도별 속도 설정 (여기서 직접 조절하세요!)")]
    public float easySpeed = 0.5f;     // Easy 난이도 속도
    public float normalSpeed = 2.5f;   // Normal 난이도 속도
    public float hardcoreSpeed = 30f;  // Hardcore 난이도 속도

    private float currentSpeed;        // 게임 시작 시 최종적으로 적용될 실제 속도

    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // 1. "Player" 태그를 가진 오브젝트를 찾아서 타겟으로 잡습니다.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        // 2. 좀비 자신의 SpriteRenderer 컴포넌트를 가져옵니다.
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ==========================================================
        // [★수정된 부분] 다른 코드들과 똑같이 GameSettings 숫자로 통일합니다!
        // ==========================================================
        int difficulty = GameSettings.currentDifficulty;

        // 4. 숫자 값(0: 이지, 1: 노말, 2: 하드코어)에 따라 속도를 매칭합니다.
        if (difficulty == 0)
        {
            currentSpeed = easySpeed;
        }
        else if (difficulty == 1)
        {
            currentSpeed = normalSpeed;
        }
        else if (difficulty == 2)
        {
            currentSpeed = hardcoreSpeed;
        }
        // ==========================================================
    }

    void Update()
    {
        if (playerTransform == null) return;

        // --- [이동 로직] ---
        // 플레이어 방향으로 난이도에 맞춰진 currentSpeed로 이동
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        transform.Translate(direction * currentSpeed * Time.deltaTime);

        // --- [방향 반전 로직] ---
        // 플레이어가 좀비보다 오른쪽에 있는지 왼쪽에 있는지 판단해서 이미지 뒤집기
        if (spriteRenderer != null)
        {
            if (playerTransform.position.x > transform.position.x)
            {
                // 플레이어가 좀비보다 오른쪽에 있으면 -> 오른쪽을 봐야 함
                spriteRenderer.flipX = true;
            }
            else if (playerTransform.position.x < transform.position.x)
            {
                // 플레이어가 좀비보다 왼쪽에 있으면 -> 원본 유지(왼쪽 봄)
                spriteRenderer.flipX = false;
            }
        }
    }
}