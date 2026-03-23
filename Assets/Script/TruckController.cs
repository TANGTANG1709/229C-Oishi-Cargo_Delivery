using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TruckController : MonoBehaviour
{
    public Rigidbody rb;

    public float acceleration = 10f;
    public float mass = 200f;
    public float turnSpeed = 90f;

    private float force;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.mass = mass;

        // กันรถคว่ำ
        rb.centerOfMass = new Vector3(0, -0.8f, 0);
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float move = 0f;

        if (Input.GetKey(KeyCode.W))
            move = 1f;

        if (Input.GetKey(KeyCode.S))
            move = -1f;

        float turn = Input.GetAxis("Horizontal");

        // 🔥 คำนวณแรง
        force = mass * acceleration;

        // 🔥 ใช้ ForceMode.Acceleration จะนิ่งกว่า
        rb.AddForce(transform.forward * move * acceleration, ForceMode.Acceleration);

        // 🔥 หมุน
        transform.Rotate(Vector3.up * turn * turnSpeed * Time.fixedDeltaTime);
    }
}