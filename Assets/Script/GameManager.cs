using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TruckController truck;
    public GameUIManager uiManager;

    void Start()
    {
     
        int diff = GameSettings.difficultyLevel;


        if (diff == 0) 
        {
            truck.mass = 100f;
            Debug.Log("เริ่มเกมด้วยโหมด: EASY");
        }
        else if (diff == 1) 
        {
            truck.mass = 200f;
            Debug.Log("เริ่มเกมด้วยโหมด: NORMAL");
        }
        else if (diff == 2) 
        {
            truck.mass = 300f;
            Debug.Log("เริ่มเกมด้วยโหมด: HARD");
        }

    
        truck.rb.mass = truck.mass;
    }
}