using UnityEngine;
using TMPro; // UI 텍스트를 다루기 위해 필수

public class GameManager : MonoBehaviour
{
    [Header("UI 설정")]
    public TextMeshProUGUI difficultyText; // 화면에 띄울 텍스트

    void Start()
    {
        // 1. 넘어온 난이도 값을 확인합니다.
        int difficulty = GameSettings.currentDifficulty;

        // 2. 값에 따라 텍스트 글자와 색깔만 바꿔줍니다.
        if (difficulty == 0)
        {
            difficultyText.text = "난이도: EASY";
            difficultyText.color = Color.green;
        }
        else if (difficulty == 1)
        {
            difficultyText.text = "난이도: NORMAL";
            difficultyText.color = Color.white;
        }
        else if (difficulty == 2)
        {
            difficultyText.text = "난이도: HARDCORE";
            difficultyText.color = Color.red;
        }
    }
}