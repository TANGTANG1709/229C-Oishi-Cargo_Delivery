using UnityEngine;

public class IceSurface : MonoBehaviour
{
    public float iceDrag = 0.05f;        // 🔥 ลื่นจัด
    public float iceAngularDrag = 0.2f;  // 🔥 หมุนง่าย

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearDamping = iceDrag;
                rb.angularDamping = iceAngularDrag;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearDamping = 0.5f;         // 🔥 กลับปกติ
                rb.angularDamping = 5f;
            }
        }
    }
}