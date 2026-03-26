using UnityEngine;
using UnityEngine.UI; // ใช้สำหรับ RawImage
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public Text timeText;
    public Text distanceText;

    [Header("ระบบหัวใจ (UI RawImages)")]
    // 1. เปลี่ยนเป็น RawImage ให้ตรงกับที่คุณใช้
    public RawImage[] heartImages;

    [Header("UI ระดับความยาก")]
    public TextMeshProUGUI difficultyText;

    public GameObject winPanel;
    public Transform player;

    [Header("เป้าหมาย/ปลายทาง")]
    public Transform destination;

    [Header("UI แพ้เกม")]
    public GameObject gameOverPanel;

    private float timer = 0f;
    private bool isTimerRunning = true;

    void Start()
    {
        UpdateDifficultyDisplay();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            UpdateTimerDisplay();

            if (destination != null && player != null)
            {
                float distanceLeft = Vector3.Distance(player.position, destination.position);
                distanceText.text = distanceLeft.ToString("F1") + " m";

                if (distanceLeft <= 2f)
                {
                    ShowWin();
                }
            }
        }
    }

    void UpdateDifficultyDisplay()
    {
        if (difficultyText == null) return;

        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Easy") GameSettings.difficultyLevel = 0;
        else if (currentScene == "Normal") GameSettings.difficultyLevel = 1;
        else if (currentScene == "Hard") GameSettings.difficultyLevel = 2;

        switch (GameSettings.difficultyLevel)
        {
            case 0:
                difficultyText.text = "Easy (Cargo 100kg)";
                difficultyText.color = Color.green;
                break;
            case 1:
                difficultyText.text = "Normal (Cargo 200kg)";
                difficultyText.color = Color.yellow;
                break;
            case 2:
                difficultyText.text = "Hard (Cargo 300kg)";
                difficultyText.color = Color.red;
                break;
            default:
                difficultyText.text = "Level: Unknown";
                difficultyText.color = Color.white;
                break;
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer % 60F);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // 2. ปรับให้รับค่าทั้ง "เลือดปัจจุบัน" และ "เลือดสูงสุด"
    public void UpdateHearts(int currentHearts, int maxHearts)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            // ถ้า Index นี้น้อยกว่า Max Hearts แปลว่าเป็นหลอดเลือดที่ใช้งานได้ในด่านนี้
            if (i < maxHearts)
            {
                heartImages[i].gameObject.SetActive(true); // เปิดให้มองเห็นหลอดเลือด

                // เช็คว่าเลือดปัจจุบันยังมีอยู่ไหม
                if (i < currentHearts)
                {
                    heartImages[i].color = Color.white; // สีปกติ (เลือดเต็ม)
                }
                else
                {
                    // เลือดลดไปแล้ว ให้เปลี่ยนเป็นสีดำโปร่งแสง (หรือสีเทา) จะได้รู้ว่าหลอดว่างเปล่า
                    heartImages[i].color = new Color(0, 0, 0, 0.5f);
                }
            }
            // ถ้าเกิน Max Hearts (เช่น โหมด Hard มีแค่ 2 เลือด ดวงที่ 3 จะเข้าเงื่อนไขนี้)
            else
            {
                heartImages[i].gameObject.SetActive(false); // ซ่อนหลอดเลือดนี้ไปเลย
            }
        }
    }

    public void ShowWin()
    {
        if (!isTimerRunning) return;
        isTimerRunning = false;
        if (winPanel != null) winPanel.SetActive(true);
        StartCoroutine(WaitAndLoadCreditScene());
    }

    private IEnumerator WaitAndLoadCreditScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Credits");
    }

    public void ShowGameOverPanel()
    {
        isTimerRunning = false;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}