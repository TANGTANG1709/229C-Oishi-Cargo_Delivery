using UnityEngine;

public class RotatingTrap : MonoBehaviour
{
    [Header("ตั้งค่าการหมุน (Rotation)")]
    [Tooltip("ความเร็วในการหมุนรอบแกน Y")]
    public float rotationSpeed = 50f;

    [Header("ตั้งค่าการลอยขึ้นลง (Bobbing)")]
    [Tooltip("ความเร็วในการลอยขึ้นและลง")]
    public float bobbingSpeed = 2f;
    [Tooltip("ระยะความสูงที่ลอยขึ้นและลง")]
    public float bobbingHeight = 0.5f;

    // เก็บตำแหน่งเริ่มต้นของวัตถุไว้ เพื่อให้มันลอยขึ้นลงจากจุดเดิมเสมอ
    private Vector3 startPosition;

    void Start()
    {
        // บันทึกตำแหน่งเริ่มต้นตอนเริ่มเกม
        startPosition = transform.position;
    }

    void Update()
    {
        // 1. ทำให้วัตถุหมุนรอบแกน Y (แกนตั้ง)
        // ใช้ Space.World เพื่อให้มันหมุนตั้งตรงเสมอ แม้ว่าตัววัตถุจะถูกจับเอียงไว้ก็ตาม
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        // 2. ทำให้วัตถุลอยขึ้นและลง
        // Mathf.Sin() จะคืนค่าสลับไปมาระหว่าง -1 ถึง 1 ตามเวลา (Time.time) ทำให้เกิดการเคลื่อนที่แบบคลื่นที่นุ่มนวล
        float newY = startPosition.y + (Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight);

        // อัปเดตตำแหน่งใหม่ให้กับวัตถุ โดยให้แกน X และ Z อยู่ที่เดิม เปลี่ยนแค่แกน Y
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
