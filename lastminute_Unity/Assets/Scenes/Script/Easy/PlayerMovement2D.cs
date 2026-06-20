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

        // 2. ★ 카메라 화면 밖으로 나가지 못하게 가두기 (Clamp) ★
        LockInCameraView();
    }

    void LockInCameraView()
    {
        // 메인 카메라가 없는 경우 에러 방지
        if (Camera.main == null) return;

        // 카메라의 시야 크기 계산하기
        float camHeight = Camera.main.orthographicSize;                  // 카메라 세로 크기의 절반
        float camWidth = camHeight * Camera.main.aspect;                 // 카메라 가로 크기의 절반 (세로 * 화면 비율)

        // 카메라의 현재 중심 위치 가져오기
        Vector3 camPos = Camera.main.transform.position;

        // 플레이어가 움직일 수 있는 최소/최대 좌표 구하기 (카메라 위치 기준)
        float minX = camPos.x - camWidth + paddingX;
        float maxX = camPos.x + camWidth - paddingX;
        float minY = camPos.y - camHeight + paddingY;
        float maxY = camPos.y + camHeight - paddingY;

        // 플레이어의 현재 위치 가져오기
        Vector3 currentPos = transform.position;

        // Mathf.Clamp(현재값, 최소값, 최대값)을 이용해 범위를 벗어나면 최소/최대값으로 고정시킵니다.
        currentPos.x = Mathf.Clamp(currentPos.x, minX, maxX);
        currentPos.y = Mathf.Clamp(currentPos.y, minY, maxY);

        // 가두어진 좌표를 플레이어에게 최종 적용
        transform.position = currentPos;
    }
}