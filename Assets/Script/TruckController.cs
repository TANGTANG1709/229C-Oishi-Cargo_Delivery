using UnityEngine;
using System.Collections;

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

    private float normalAcceleration;
    private float normalTurnSpeed;

    [Header("Audio (เสียงเครื่องยนต์)")]
    public AudioSource truckAudioSource;
    public AudioClip forwardSound;
    public AudioClip reverseSound;

    // 🔥 ส่วนที่เพิ่มใหม่: ตั้งค่าการปรับ Pitch เสียงให้เนียนขึ้น
    [Header("Engine Sound Settings (แก้เสียงตัด)")]
    public float minPitch = 0.8f;        // เสียงทุ้มสุดตอนเริ่มออกตัว
    public float maxPitch = 1.5f;        // เสียงแหลมสุดตอนรถวิ่งเร็ว
    public float maxSpeedForPitch = 15f; // ความเร็วสูงสุดที่จะใช้คำนวณเสียง

    [Header("Engine System")]
    public AudioClip startEngineSound;
    public AudioClip stopEngineSound;

    private bool isEngineOn = false;
    private bool isEngineStarting = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.mass = mass;
        rb.centerOfMass = new Vector3(0, -0.8f, 0);

        normalAcceleration = acceleration;
        normalTurnSpeed = turnSpeed;

        if (truckAudioSource != null)
        {
            truckAudioSource.loop = false;
        }
    }

    void Update()
    {
        HandleEngineState();

        if (isEngineOn)
        {
            HandleEngineSound();
            UpdateEnginePitch(); // 🔥 เรียกใช้งานการปรับเสียงตามความเร็ว
        }
    }

    void FixedUpdate()
    {
        if (isEngineOn)
        {
            Move();
        }

        ApplyAirResistance();
    }

    void HandleEngineState()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isEngineOn && !isEngineStarting)
            {
                StartCoroutine(StartEngineRoutine());
            }
            else if (isEngineOn)
            {
                StopEngine();
            }
        }
    }

    IEnumerator StartEngineRoutine()
    {
        isEngineStarting = true;

        if (truckAudioSource != null && startEngineSound != null)
        {
            truckAudioSource.Stop();
            truckAudioSource.clip = startEngineSound;
            truckAudioSource.loop = false;

            // 🔥 รีเซ็ต Pitch ให้เป็นปกติก่อนเล่นเสียงสตาร์ท
            truckAudioSource.pitch = 1f;

            truckAudioSource.Play();

            yield return new WaitForSeconds(startEngineSound.length);
        }

        isEngineOn = true;
        isEngineStarting = false;

        if (truckAudioSource != null)
        {
            truckAudioSource.loop = true;
        }
    }

    void StopEngine()
    {
        isEngineOn = false;
        isEngineStarting = false;

        StopAllCoroutines();

        if (truckAudioSource != null)
        {
            truckAudioSource.Stop();
            // 🔥 รีเซ็ต Pitch ให้เป็นปกติก่อนเล่นเสียงดับเครื่อง
            truckAudioSource.pitch = 1f;

            if (stopEngineSound != null)
            {
                truckAudioSource.PlayOneShot(stopEngineSound);
            }
        }
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
        Vector3 airDrag = -rb.linearVelocity * airResistance;
        rb.AddForce(airDrag);
    }

    void HandleEngineSound()
    {
        if (truckAudioSource == null) return;

        if (Input.GetKey(KeyCode.W))
        {
            PlaySound(forwardSound);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            PlaySound(reverseSound);
        }
        else
        {
            truckAudioSource.Stop();
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        if (truckAudioSource.clip != clip)
        {
            truckAudioSource.clip = clip;
            truckAudioSource.Play();
        }
        else if (!truckAudioSource.isPlaying)
        {
            truckAudioSource.Play();
        }
    }

    // 🔥 ฟังก์ชันใหม่: ปรับระดับเสียง (Pitch) ตามความเร็วรถ
    void UpdateEnginePitch()
    {
        if (truckAudioSource == null || !truckAudioSource.isPlaying) return;

        // เช็คว่าเสียงที่เล่นอยู่คือเสียงเดินหน้าใช่ไหม?
        if (truckAudioSource.clip == forwardSound)
        {
            // หาความเร็วปัจจุบันของรถ
            float currentSpeed = rb.linearVelocity.magnitude;

            // คำนวณเทียบอัตราส่วนความเร็วกับ Pitch
            float targetPitch = Mathf.Lerp(minPitch, maxPitch, currentSpeed / maxSpeedForPitch);

            // นำค่าไปใส่ให้ Audio Source
            truckAudioSource.pitch = targetPitch;
        }
        else
        {
            // ถ้าเป็นเสียงอื่นๆ (เช่น เสียงถอยหลัง) ให้ระดับเสียงคงที่ (Pitch = 1)
            truckAudioSource.pitch = 1f;
        }
    }
}