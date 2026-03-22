using UnityEngine;

public class TruckController : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Physics")]
    public float acceleration = 10f;
    public float mass = 200f;
    private float force;

    [Header("Movement")]
    public float turnSpeed = 100f;

    void Start()
    {
        rb.mass = mass;
    }

    void Update()
    {
        Move();
        UpdateForce();
    }

    void Move()
    {
        float move = Input.GetKey(KeyCode.W) ? 1f : 0f;
        float turn = 0f;

        if (Input.GetKey(KeyCode.A)) turn = -1f;
        if (Input.GetKey(KeyCode.D)) turn = 1f;

        // คำนวณแรง F = ma
        force = mass * acceleration;

        // เดินหน้า
        rb.AddForce(transform.forward * move * force);

        // หมุน
        transform.Rotate(Vector3.up * turn * turnSpeed * Time.deltaTime);
    }

    void UpdateForce()
    {
        force = mass * acceleration;
    }

    public float GetForce()
    {
        return force;
    }
}
