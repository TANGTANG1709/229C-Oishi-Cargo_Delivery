using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public int maxHearts = 3;
    private int currentHearts;

    void Start()
    {
        currentHearts = maxHearts;
    }

    public void TakeDamage(int damage)
    {
        currentHearts -= damage;

        Debug.Log("HP: " + currentHearts);

        if (currentHearts <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Over");
    }
}