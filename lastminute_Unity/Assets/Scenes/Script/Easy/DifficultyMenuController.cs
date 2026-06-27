using UnityEngine;

public class DifficultyMenuController : MonoBehaviour
{
    [Header("초록색 설명 패널 오브젝트")]
    public GameObject explanationPanel;

    // [뒤로가기] 버튼을 눌렀을 때 실행할 함수
    public void ClosePanel()
    {
        if (explanationPanel != null)
        {
            explanationPanel.SetActive(false); // 패널 끄기
            
        }
    }
}