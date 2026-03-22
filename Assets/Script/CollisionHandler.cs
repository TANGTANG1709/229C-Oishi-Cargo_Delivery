using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public int maxHearts = 3;
    private int currentHearts;

    public GameUIManager uiManager;

    void Start()
    {
        currentHearts = maxHearts;
        uiManager.UpdateHearts(currentHearts);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            currentHearts--;

            uiManager.UpdateHearts(currentHearts);

            if (currentHearts <= 0)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Over");
    }
}