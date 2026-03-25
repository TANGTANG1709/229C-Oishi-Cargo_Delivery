using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [Header("Prefab หิน")]
    public GameObject rockPrefab;

    [Header("พื้นที่สุ่มเกิด (กว้าง x สูง x ลึก)")]
    public Vector3 spawnAreaSize = new Vector3(20f, 0f, 100f);

    [Header("ระยะห่างระหว่างหิน")]
    public float rockSpacing = 3f;

    [Header("Layer ของหิน (ตั้งค่าให้ Rock Prefab เป็น layer นี้)")]
    public LayerMask rockLayer;

    [Header("ความสูงตอนยิง Raycast")]
    public float rayStartHeight = 50f;

    [Header("ความสูงลอยจากพื้น")]
    public float spawnOffsetY = 3f;

    private int rockCount = 10;

    void Start()
    {
        int diff = GameSettings.difficultyLevel;

        if (diff == 0) rockCount = 10;
        else if (diff == 1) rockCount = 20;
        else if (diff == 2) rockCount = 30;

        Debug.Log("สุ่มสร้างหินจำนวน: " + rockCount + " ก้อน");

        SpawnRocks();
    }

    void SpawnRocks()
    {
        for (int i = 0; i < rockCount; i++)
        {
            Vector3 spawnPosition = Vector3.zero;
            bool validPosition = false;
            int attempts = 0;

            while (!validPosition && attempts < 100)
            {
                float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
                float randomZ = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);

                // ยิง Ray ลงหาพื้น
                Vector3 rayStart = new Vector3(transform.position.x + randomX, rayStartHeight, transform.position.z + randomZ);

                RaycastHit hit;

                if (Physics.Raycast(rayStart, Vector3.down, out hit, 100f))
                {
                    spawnPosition = hit.point + Vector3.up * spawnOffsetY;

                    // เช็คเฉพาะหิน ไม่เช็คพื้น
                    if (!Physics.CheckSphere(spawnPosition, rockSpacing, rockLayer))
                    {
                        validPosition = true;
                    }
                }

                attempts++;
            }

            if (validPosition)
            {
                Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("พื้นที่แน่นเกินไป สร้างหินไม่ครบ!");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        Gizmos.DrawCube(transform.position, spawnAreaSize);
    }
}