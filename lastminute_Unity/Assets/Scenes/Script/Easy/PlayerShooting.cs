using UnityEngine;
using UnityEngine.InputSystem; // 새로운 입력 시스템 사용

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 15f; // 총알 속도

    [Header("연사 속도 제한 (쿨타임)")]
    public float fireRate = 0.2f;    // [★조절가능] 다음 총알이 나갈 때까지의 시간 (0.2초에 1발)
    private float cooldownTimer = 0f; // 쿨타임을 실시간으로 계산할 변수

    [Header("사운드 설정")]
    public AudioClip gunshotSound;   // 재생할 총소리 파일 (.wav)
    private AudioSource audioSource; // 오디오를 재생할 스피커 컴포넌트

    void Start()
    {
        // 플레이어 오브젝트에 있는 Audio Source 컴포넌트를 자동으로 가져옵니다.
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Mouse.current == null) return;

        // 1. 매 프레임마다 쿨타임 타이머를 실시간으로 줄여줍니다.
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // 2. 마우스 왼쪽 버튼을 눌렀고, 동시에 '쿨타임이 0 이하'일 때만 Shoot 함수 실행
        if (Mouse.current.leftButton.wasPressedThisFrame && cooldownTimer <= 0f)
        {
            Shoot();

            // 총을 쐈으므로 설정한 연사 속도만큼 쿨타임을 다시 충전합니다.
            cooldownTimer = fireRate;
        }
    }

    void Shoot()
    {
        // 총을 쏠 때 총소리를 재생하는 함수 호출
        PlayGunshot();

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

    // 오디오 소스와 오디오 클립이 잘 연결되어 있으면 소리를 재생하는 함수
    void PlayGunshot()
    {
        if (audioSource != null && gunshotSound != null)
        {
            // PlayOneShot을 쓰면 이전 총소리가 끝나기 전에 또 쏴도 소리가 자연스럽게 겹쳐서 납니다!
            audioSource.PlayOneShot(gunshotSound);
        }
    }
}