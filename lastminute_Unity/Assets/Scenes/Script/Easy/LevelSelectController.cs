using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{
    // 이 함수들을 각각의 버튼 On Click()에 연결해 줍니다.

    public void SelectEasy()
    {
        GameSettings.currentDifficulty = 0; // Easy 저장

        SceneManager.LoadScene("PlayScene_Easy");
    }

    public void SelectNormal()
    {
        GameSettings.currentDifficulty = 1; // Normal 저장
        SceneManager.LoadScene("PlayScene_Normal");
    }

    public void SelectHardcore()
    {
        GameSettings.currentDifficulty = 2; // Hardcore 저장
        SceneManager.LoadScene("PlayScene_Hardcore");
    }

}