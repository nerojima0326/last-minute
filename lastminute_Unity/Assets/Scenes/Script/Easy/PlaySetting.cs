using UnityEngine;
using TMPro; // ★ UI 텍스트(TextMeshPro)를 제어하기 위해 반드시 추가해야 합니다!

public class PlaySetting : MonoBehaviour
{
    [Header("UI 연결")]
    // 유니티 인스펙터 창에서 텍스트를 집어넣을 수 있는 빈칸을 만듭니다.
    public TextMeshProUGUI difficultyText;

    void Start()
    {
        if (GameSettings.currentDifficulty == 0)
        {
            Debug.Log("현재 난이도는 EASY입니다.");

            // 화면의 텍스트와 색깔을 실제로 바꿔주는 부분
            difficultyText.text = "EASY";
            difficultyText.color = Color.green;
        }
        else if (GameSettings.currentDifficulty == 1)
        {
            Debug.Log("현재 난이도는 NORMAL입니다.");

            difficultyText.text = "NORMAL";
            difficultyText.color = Color.white;
        }
        else if (GameSettings.currentDifficulty == 2)
        {
            Debug.Log("현재 난이도는 HARDCORE입니다.");

            difficultyText.text = "HARDCORE";
            difficultyText.color = Color.red;
        }
    }
}