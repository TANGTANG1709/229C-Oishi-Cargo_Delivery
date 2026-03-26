using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    
    private int maxHearts;
    private int currentHearts;

    [Header("อ้างอิง UI Manager")]
    public GameUIManager uiManager;

    void Start()
    {
        switch (GameSettings.difficultyLevel)
        {
            case 0: 
                maxHearts = 3;
                break;
            case 1:
                maxHearts = 3;
                break;
            case 2: 
                maxHearts = 2;
                break;
            default:
                maxHearts = 3;
                break;
        }

        currentHearts = maxHearts;

        if (uiManager != null)
        {
            uiManager.UpdateHearts(currentHearts);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHearts -= damage;

        Debug.Log("HP: " + currentHearts);

        if (uiManager != null)
        {
            uiManager.UpdateHearts(currentHearts);
        }

        if (currentHearts <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");


        Time.timeScale = 0f;

    
        if (uiManager != null)
        {
            uiManager.ShowGameOverPanel();
        }


        Destroy(gameObject);
    }
}