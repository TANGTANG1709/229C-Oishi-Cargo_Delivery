using UnityEngine;

public class IceSurface : MonoBehaviour
{
    public float iceDrag = 0.1f;        // ลื่นมาก
    public float iceAngularDrag = 0.5f; // หมุนง่าย

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
}
