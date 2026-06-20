using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 15f; // 총알 속도 (조금 더 시원하게 올렸습니다)

    void Update()
    {
        if (Mouse.current == null) return;

        // 마우스 왼쪽 버튼을 누른 바로 그 프레임에만 Shoot 함수 실행
        if (Mouse.current.leftButton.wasPressedThisFrame)
            Shoot();
    }

    void Shoot()
    {
        if (bulletPrefab == null) return;

        // 1. 마우스 화면 좌표를 게임 세상의 정확한 2D 좌표로 변환
        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        // 2. 플레이어 중심에서 마우스를 조준하는 방향 계산
        Vector2 shootDirection = (mouseWorldPos - transform.position).normalized;

        // 3. 플레이어 현재 위치에 총알 생성
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // 4. 생성된 총알에 방향과 속도 주입
        Bullet2D bulletScript = bullet.GetComponent<Bullet2D>();
        if (bulletScript != null)
            bulletScript.SetDirection(shootDirection, bulletSpeed);
    }
}