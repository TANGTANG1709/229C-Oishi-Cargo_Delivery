using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [Header("ตั้งค่าหิน (ลาก Prefab หินมาใส่ตรงนี้)")]
    public GameObject rockPrefab;

    [Header("ขนาดพื้นที่สุ่มเกิด (กว้าง x สูง x ลึก)")]
    public Vector3 spawnAreaSize = new Vector3(20f, 0f, 100f);

    [Header("ระยะห่างระหว่างหิน (ป้องกันการเกิดทับกัน)")]
    public float rockSpacing = 3f; // ปรับค่านี้ให้ใหญ่กว่าขนาดหินนิดหน่อย

    private int rockCount = 10;

    void Start()
    {
        int diff = GameSettings.difficultyLevel;

        if (diff == 0) rockCount = 10; // Easy
        else if (diff == 1) rockCount = 20; // Normal
        else if (diff == 2) rockCount = 30; // Hard

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

            // สุ่มตำแหน่งใหม่เรื่อยๆ จนกว่าจะเจอที่ว่าง หรือสุ่มเกิน 100 ครั้ง (กันเครื่องค้าง)
            while (!validPosition && attempts < 100)
            {
                float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
                float randomZ = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);
                float spawnY = transform.position.y;

                spawnPosition = new Vector3(transform.position.x + randomX, spawnY, transform.position.z + randomZ);

                // 🌟 หัวใจสำคัญ: เช็คว่าในรัศมี rockSpacing มี Collider อะไรอยู่ไหม
                // ถ้าไม่มี (!Physics.CheckSphere) แปลว่าตำแหน่งนี้ว่าง ให้ validPosition = true เพื่อหลุดลูป
                if (!Physics.CheckSphere(spawnPosition, rockSpacing))
                {
                    validPosition = true;
                }

                attempts++;
            }

            // ถ้าหาที่ว่างได้สำเร็จ ก็สร้างหินเลย
            if (validPosition)
            {
                Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("พื้นที่แน่นเกินไป สร้างหินไม่ครบจำนวนที่ตั้งไว้!");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        Gizmos.DrawCube(transform.position, spawnAreaSize);
    }
}
