using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections; 

public class FinishLine : MonoBehaviour
{
    public GameUIManager uiManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FinishGame();
        }
    }

    void FinishGame()
    {
        Time.timeScale = 0f; 

        Debug.Log("You Win!");

        if (uiManager != null)
        {
            uiManager.ShowWin();
        }

   
        StartCoroutine(WaitAndLoadCredits());
    }


    private IEnumerator WaitAndLoadCredits()
    {
     
        yield return new WaitForSecondsRealtime(2f);

 
        Time.timeScale = 1f;

     
        SceneManager.LoadScene("Credits");
    }
}