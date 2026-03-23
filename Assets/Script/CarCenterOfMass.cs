using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarCenterOfMass : MonoBehaviour
{

    [Tooltip("ใส่ Empty GameObject ที่จะเป็นจุดศูนย์ถ่วงใหม่ไว้ตรงนี้")]
    public Transform centerOfMass;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // ตรวจสอบว่าได้กำหนดจุดศูนย์ถ่วงมาให้หรือไม่
        if (centerOfMass != null)
        {
            // กำหนดให้ Center of Mass ของ Rigidbody เปลี่ยนไปอยู่ที่ตำแหน่งของ Transform ที่เรากำหนดไว้
            // ต้องใช้ localPosition เพราะ Center of Mass อ้างอิงจากตำแหน่งของตัวรถ (Local Space)
            rb.centerOfMass = centerOfMass.localPosition;
        }
        else
        {
            Debug.LogWarning("อย่าลืมกำหนด Transform ให้กับ Center of Mass ใน Inspector นะครับ!");
        }
    }
}

