using UnityEngine;

public class MudSurface : MonoBehaviour
{
    public float mudDrag = 5f;          // หนืด
    public float mudAngularDrag = 5f;   // หมุนยาก

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearDamping = mudDrag;
                rb.angularDamping = mudAngularDrag;
            }
        }
    }
}