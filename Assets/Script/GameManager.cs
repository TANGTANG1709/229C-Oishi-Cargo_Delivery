using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TruckController truck;
    public GameUIManager uiManager;

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public Difficulty difficulty;

    void Start()
    {
        SetDifficulty(difficulty);
    }

    public void SetDifficulty(Difficulty diff)
    {
        switch (diff)
        {
            case Difficulty.Easy:
                truck.mass = 100f;
                break;

            case Difficulty.Medium:
                truck.mass = 200f;
                break;

            case Difficulty.Hard:
                truck.mass = 300f;
                break;
        }

        float force = truck.mass * truck.acceleration;

        
    }
}
