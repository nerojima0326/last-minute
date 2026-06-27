using UnityEngine;
using UnityEngine.InputSystem; // 새로운 입력 시스템 사용

public class PlayerMovement2D : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 6.0f;

    [Header("플레이어 크기 보정")]
    // 캐릭터 중심점 기준이라 벽에 절반쯤 파묻힐 수 있어서, 캐릭터 크기의 절반 만큼 여유(패딩)를 줍니다.
    public float paddingX = 0.5f;
    public float paddingY = 0.5f;

    private SpriteRenderer spriteRenderer;
    private Animator anim;

    void Start()
    {
        // 게임 시작될 때 내 플레이어에 있는 컴포넌트들을 가져옵니다.
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. 키보드 입력 및 이동 처리
        if (Keyboard.current == null) return;

        float moveX = 0f;
        float moveY = 0f;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveY = 1f;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveY = -1f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveX = -1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveX = 1f;

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // ==========================================
        // [수정된 부분] 좌우 반전 처리 (원본이 왼쪽을 보는 이미지일 때)
        // ==========================================
        if (spriteRenderer != null)
        {
            if (moveX < 0)
            {
                spriteRenderer.flipX = false; // 왼쪽(A키)으로 가면 원본 유지 (왼쪽 봄)
            }
            else if (moveX > 0)
            {
                spriteRenderer.flipX = true;  // 오른쪽(D키)으로 가면 이미지 뒤집기 (오른쪽 봄)
            }
        }

        // 달리기/대기 애니메이션 전환
        if (anim != null)
        {
            if (moveX != 0 || moveY != 0)
            {
                anim.SetBool("isRun", true);  // 달리기 애니메이션 켜기
            }
            else
            {
                anim.SetBool("isRun", false); // 아무것도 안 누르면 대기 상태로
            }
        }
        // ==========================================

        // 2. ★ 카메라 화면 밖으로 나가지 못하게 가두기 (Clamp) ★
        LockInCameraView();
    }

    void LockInCameraView()
    {
        if (Camera.main == null) return;

        // 카메라의 시야 크기 계산하기
        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;

        // 카메라의 현재 중심 위치 가져오기
        Vector3 camPos = Camera.main.transform.position;

        // 플레이어가 움직일 수 있는 최소/최대 좌표 구하기 (카메라 위치 기준)
        float minX = camPos.x - camWidth + paddingX;
        float maxX = camPos.x + camWidth - paddingX;
        float minY = camPos.y - camHeight + paddingY;
        float maxY = camPos.y + camHeight - paddingY;

        // 플레이어의 현재 위치 가져오기
        Vector3 currentPos = transform.position;

        // Mathf.Clamp를 이용해 범위를 벗어나면 최소/최대값으로 고정시킵니다.
        currentPos.x = Mathf.Clamp(currentPos.x, minX, maxX);
        currentPos.y = Mathf.Clamp(currentPos.y, minY, maxY);

        // 가두어진 좌표를 플레이어에게 최종 적용
        transform.position = currentPos;
    }
}