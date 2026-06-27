using UnityEngine;
using TMPro; // TextMeshPro 제어용

public class GameTimer : MonoBehaviour
{
    private float currentTime;
    private bool isGameFinished = false;

    [Header("UI 연결")]
    // 우리가 방금 만든 TimerText 오브젝트를 넣을 칸입니다.
    public GameObject timerTextObject;
    private TMP_Text timerTextComponent;

    // 유니티 에디터에서 승리 패널을 드래그해서 넣어줄 빈칸을 만듭니다.
    public GameObject victoryPanel;

    // [★추가] 유니티 에디터에서 패배 패널을 드래그해서 넣어줄 빈칸을 만듭니다.
    public GameObject defeatPanel;

    void Start()
    {
        // 오브젝트 안에서 진짜 글자를 바꾸는 부품(TMP_Text)을 쏙 골라냅니다.
        if (timerTextObject != null)
        {
            timerTextComponent = timerTextObject.GetComponent<TMP_Text>();
        }

        // 게임 시작할 때 승리 패널은 확실하게 꺼둡니다.
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }

        // [★추가] 게임 시작할 때 패배 패널도 확실하게 꺼둡니다.
        if (defeatPanel != null)
        {
            defeatPanel.SetActive(false);
        }

        switch (GameSettings.currentDifficulty)
        {
            case 0:
                currentTime = 60f;
                break;
            case 1:
                currentTime = 180f;
                break;
            case 2:
                currentTime = 300f;
                break;
        }

        UpdateTimerText();
    }

    void Update()
    {
        // 승리했거나 패배해서 게임이 이미 끝났다면 타이머 진행을 막습니다.
        if (isGameFinished) return;

        // 시간이 남아있다면 실시간으로 시간을 줄입니다.
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            // 0초 이하로 떨어지면 딱 0으로 고정하고 승리 함수를 부릅니다.
            if (currentTime <= 0)
            {
                currentTime = 0;
                WinGame();
            }

            UpdateTimerText();
        }
    }

    // 숫자를 우리가 아는 "분:초 (00:00)" 형태로 예쁘게 그려주는 함수
    void UpdateTimerText()
    {
        if (timerTextComponent == null) return;

        int minutes = Mathf.FloorToInt(currentTime / 60f); // 전체 초를 60으로 나눈 '분'
        int seconds = Mathf.FloorToInt(currentTime % 60f); // 60으로 나누고 남은 '초'

        // {0:00}은 자릿수가 한 자리일 때 앞에 0을 채워서 무조건 두 자리로 만들라는 뜻입니다. (예: 5초 -> 05초)
        timerTextComponent.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // 제한시간을 버텼을 때 실행되는 승리 함수
    void WinGame()
    {
        isGameFinished = true;
        Debug.Log("★ 축하합니다! 제한시간 버티기 성공! 승리! ★");

        // 게임을 정지시킵니다.
        Time.timeScale = 0f;

        // 숨겨두었던 승리 패널을 화면에 탁! 켜줍니다.
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
    }

    // [★추가] 체력이 0이 되었을 때 외부(따로 만든 체력 스크립트)에서 호출해 줄 패배 함수
    public void LoseGame()
    {
        // 이미 승리해서 게임이 끝난 상태라면 패배 처리가 중복으로 실행되지 않게 막습니다.
        if (isGameFinished) return;

        isGameFinished = true;
        Debug.Log("💀 플레이어 사망... 패배! 💀");

        // 게임을 정지시킵니다. (좀비, 총알 다 멈춤)
        Time.timeScale = 0f;

        // 숨겨두었던 패배 패널을 화면에 탁! 켜줍니다.
        if (defeatPanel != null)
        {
            defeatPanel.SetActive(true);
        }
    }
}