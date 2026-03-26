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
                maxHearts = 2; // Hard โหมดมี 2 เลือด
                break;
            default:
                maxHearts = 3;
                break;
        }

        currentHearts = maxHearts;

        if (uiManager != null)
        {
            // ส่งไปทั้งเลือดปัจจุบัน และเลือดสูงสุด
            uiManager.UpdateHearts(currentHearts, maxHearts);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHearts -= damage;
        Debug.Log("HP: " + currentHearts);

        if (uiManager != null)
        {
            // อัปเดต UI เมื่อโดนดาเมจ
            uiManager.UpdateHearts(currentHearts, maxHearts);
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

        Destroy(gameObject); // ระวังนิดนึงนะครับ ถ้า Destroy ตัวเองไปแล้ว โค้ดอื่นที่อ้างอิง player อาจจะ Error ได้
    }
}