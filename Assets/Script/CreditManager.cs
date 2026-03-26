using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{
 
    public void GoToMenu()
    {
        
        SceneManager.LoadScene("Menu");
    }

    
    public void ExitGame()
    {
        Debug.Log("กำลังออกจากเกม...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}