using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // ★ 새로운 입력 시스템을 쓰기 위해 추가해야 합니다.

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;

    void Update()
    {
        // ★ 예전 Input.GetKeyDown 대신 새로운 키보드 감지 코드를 사용합니다.
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene"); // 씬 이름 확인 필수
    }
}