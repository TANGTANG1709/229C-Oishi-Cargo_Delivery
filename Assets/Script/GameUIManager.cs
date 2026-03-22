using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public Text massText;
    public Text accelText;
    public Text forceText;
    public Text timeText;
    public Text distanceText;
    public Text heartsText;

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

    public void UpdatePhysics(float mass, float accel, float force)
    {
        massText.text = "MASS: " + mass + " kg";
        accelText.text = "ACCEL: " + accel;
        forceText.text = "FORCE: " + force + " N";
    }

    public void UpdateHearts(int hearts)
    {
        heartsText.text = "❤️: " + hearts;
    }
}
