using UnityEngine;

using UnityEngine.SceneManagement; 

public class SceneController : MonoBehaviour
{
    
    public void GoToMainScene()
    {
        
        SceneManager.LoadScene("MaingameScene"); 
    }
}