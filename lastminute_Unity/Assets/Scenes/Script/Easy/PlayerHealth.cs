using UnityEngine;
using TMPro; // TextMeshPro 제어용

public class PlayerHealth : MonoBehaviour
{
    [Header("체력 설정")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("체력 UI (텍스트 방식)")]
    public GameObject hpTextObject;

    private TMP_Text hpTextComponent;
    private float cooldownTimer = 0f;
    public float damageCooldown = 0.5f;

    void Start()
    {
        currentHealth = maxHealth;

        if (hpTextObject != null)
        {
            hpTextComponent = hpTextObject.GetComponent<TMP_Text>();
        }

        UpdateHPText();
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        if (cooldownTimer > 0) return;

        currentHealth -= damage;
        cooldownTimer = damageCooldown;

        UpdateHPText();

        Debug.Log($"플레이어 피격! 남은 체력: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHPText()
    {
        if (hpTextComponent != null)
        {
            hpTextComponent.text = $"HP: {currentHealth}/{maxHealth}";
        }
    }

    void Die()
    {
        Debug.Log("★ 플레이어 사망! 게임 오버 ★");
        gameObject.SetActive(false);
    }

    // ★ 중요: OnCollisionStay2D를 OnCollisionEnter2D로 바꾸고, Destroy 코드를 추가했습니다!
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 부딪힌 상대방의 태그가 "Enemy"(좀비) 라면
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);                 // 1. 플레이어 체력을 10 깎고 무적 타이머 가동
            Destroy(collision.gameObject);   // 2. ★ 부딪힌 좀비 오브젝트를 즉시 게임에서 삭제! ★
        }
    }
}