using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TruckController : MonoBehaviour
{
    public Rigidbody rb;

    public float acceleration = 10f;
    public float mass = 200f;
    public float turnSpeed = 90f;

    private float force;

    [Header("Advanced Physics")]
    public float airResistance = 0.5f;

    // 🔥 เพิ่ม (เก็บค่าเดิมไว้)
    private float normalAcceleration;
    private float normalTurnSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.mass = mass;

        // กันรถคว่ำ
        rb.centerOfMass = new Vector3(0, -0.8f, 0);

        // 🔥 เพิ่ม (จำค่าเดิม)
        normalAcceleration = acceleration;
        normalTurnSpeed = turnSpeed;
    }

    void FixedUpdate()
    {
        Move();
        ApplyAirResistance();
    }

    void Move()
    {
        float move = 0f;
        if (Input.GetKey(KeyCode.W)) move = 1f;
        if (Input.GetKey(KeyCode.S)) move = -1f;

        float turn = Input.GetAxis("Horizontal");

        float calculatedForce = mass * acceleration;

        rb.AddForce(transform.forward * move * calculatedForce, ForceMode.Force);

        transform.Rotate(Vector3.up * turn * turnSpeed * Time.fixedDeltaTime);
    }

    void ApplyAirResistance()
    {
        // 🔥 แก้เล็กน้อย (linearVelocity → velocity)
        Vector3 airDrag = -rb.linearVelocity * airResistance;
        rb.AddForce(airDrag);
    }
}