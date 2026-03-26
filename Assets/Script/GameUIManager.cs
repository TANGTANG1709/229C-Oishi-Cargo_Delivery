using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro; // 1. เพิ่ม Library สำหรับใช้งาน TextMeshPro

public class GameUIManager : MonoBehaviour
{
    public Text timeText;
    public Text distanceText;
    public Text heartsText; // (แนะนำให้เปลี่ยนเป็น TextMeshProUGUI ในอนาคตถ้าหัวใจยังไม่ขึ้นนะครับ)

    [Header("UI ระดับความยาก")]
    public TextMeshProUGUI difficultyText; // 2. ประกาศตัวแปร TextMeshPro สำหรับแสดงความยาก

    public GameObject winPanel;
    public Transform player;

    [Header("เป้าหมาย/ปลายทาง")]
    public Transform destination;

    private float timer = 0f;
    private bool isTimerRunning = true;

    void Start()
    {
        // 3. เรียกใช้ฟังก์ชันอัปเดตระดับความยากตอนเริ่มเกม
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

        // เช็คจาก GameSettings.difficultyLevel ที่รับมาจากหน้า Menu (0=Easy, 1=Normal, 2=Hard)
        switch (GameSettings.difficultyLevel)
        {
            case 0: // Easy
                difficultyText.text = "Easy (Cargo 100kg)";
                difficultyText.color = Color.green;
                break;
            case 1: // Normal
                difficultyText.text = "Normal (Cargo 200kg)";
                difficultyText.color = Color.yellow;
                break;
            case 2: // Hard
                difficultyText.text = "Hard (Cargo 300kg)";
                difficultyText.color = Color.red;
                break;
            default:
                // เผื่อไว้กรณีมีค่าแปลกๆ
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

    public void UpdateHearts(int hearts)
    {
        if (heartsText != null)
        {
            string heartsDisplay = "";
            for (int i = 0; i < hearts; i++)
            {
               
                heartsDisplay += "❤️";
            }
            heartsText.text = heartsDisplay;
        }
    }

    public void ShowWin()
    {
        if (!isTimerRunning) return;

        isTimerRunning = false;

        if (winPanel != null)
            winPanel.SetActive(true);

        StartCoroutine(WaitAndLoadCreditScene());
    }

    private IEnumerator WaitAndLoadCreditScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Credits");
    }
}