using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections; 

public class GameUIManager : MonoBehaviour
{
    public Text timeText;
    public Text distanceText;
    public Text heartsText;

    public GameObject winPanel;
    public Transform player;

    [Header("เป้าหมาย/ปลายทาง")]
    public Transform destination;

    private float timer = 0f;
    private bool isTimerRunning = true;

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