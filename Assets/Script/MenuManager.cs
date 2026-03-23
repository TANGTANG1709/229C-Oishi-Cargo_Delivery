using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuManager : MonoBehaviour
{
  
    public void GoToDifficultyScene()
    {
  
        SceneManager.LoadScene("DifficultySelection");
    }

    public void SelectEasy()
    {
        GameSettings.difficultyLevel = 0; 
        SceneManager.LoadScene("Game");   
    }

    public void SelectNormal()
    {
        GameSettings.difficultyLevel = 1;
        SceneManager.LoadScene("Game");
    }

    public void SelectHard()
    {
        GameSettings.difficultyLevel = 2;
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {


#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;


#else
            Application.Quit();
#endif
    }
}