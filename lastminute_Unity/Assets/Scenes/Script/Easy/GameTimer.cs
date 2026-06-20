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

    void Start()
    {
        // 오브젝트 안에서 진짜 글자를 바꾸는 부품(TMP_Text)을 쏙 골라냅니다.
        if (timerTextObject != null)
        {
            timerTextComponent = timerTextObject.GetComponent<TMP_Text>();
        }

        switch(GameSettings.currentDifficulty)
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

    // 1분을 버텼을 때 실행되는 승리 함수
    void WinGame()
    {
        isGameFinished = true;
        Debug.Log("★ 축하합니다! 1분 버티기 성공! 승리! ★");

        // 게임을 정지시킵니다. (나중에 여기에 '승리 팝업창 켜기' 코드를 넣으면 됩니다.)
        Time.timeScale = 0f;
    }
}