using UnityEngine;

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
    }
}
