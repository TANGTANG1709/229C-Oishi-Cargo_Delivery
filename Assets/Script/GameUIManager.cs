using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public Text timeText;
    public Text distanceText;
    public Text heartsText;

    public GameObject winPanel;
    public Transform player;
    private Vector3 startPos;

    private float timer = 0f;

    void Start()
    {
        startPos = player.position;
    }

    void Update()
    {
        timer += Time.deltaTime;

        float distance = Vector3.Distance(startPos, player.position);

        timeText.text = "Time: " + timer.ToString("F1");
        distanceText.text = "Distance: " + distance.ToString("F1") + " m";
    }



    public void UpdateHearts(int hearts)
    {
        if (heartsText != null)
            heartsText.text = "❤️: " + hearts;
    }



    public void ShowWin()
    {
        if (winPanel != null)
            winPanel.SetActive(true);
    }
}

