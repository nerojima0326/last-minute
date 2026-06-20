using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;    // 생성할 좀비 원본 프리팹
    public int InitialCount = 5;  // 게임 시작하자마자 깔고 시작할 좀비 수
    public float spawnInterval = 3.0f; // ★ 다음 좀비가 나올 시간 (초 단위! 인스펙터에서 조절 가능)
    public float spawnDistance = 12f; // 카메라 시야 밖 거리

    private float timer = 0f;         // 시간을 잴 타이머
    private GameObject player;

    void Start()
    {
        // 플레이어를 태그로 미리 찾아둡니다.
        player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < InitialCount; i++)
        {
            SpawnSingleEnemy();
        }
    }

    void Update()
    {
        if (player == null) return;

        // 1. 실시간으로 흐르는 시간을 타이머에 더해줍니다.
        timer += Time.deltaTime;

        // 2. 타이머가 우리가 설정한 '스폰 시간(spawnInterval)'에 도달하면!
        if (timer >= spawnInterval)
        {
            SpawnSingleEnemy(); // 좀비 1마리 추가 생성
            timer = 0f;         // 타이머를 다시 0초로 초기화
        }
    }

    // 좀비 딱 1마리를 화면 밖에 소환하는 튼튼한 함수
    void SpawnSingleEnemy()
    {
        if (enemyPrefab == null || player == null) return;

        // 플레이어 주변의 랜덤한 각도 구하기
        float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector2 spawnDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));

        // 플레이어 위치에서 카메라 바깥 거리만큼 떨어진 좌표 계산
        Vector2 spawnPosition = (Vector2)player.transform.position + (spawnDirection * spawnDistance);

        // 적 생성
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}