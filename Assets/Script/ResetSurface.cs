using UnityEngine;

public class NormalSurface : MonoBehaviour
{
    public float normalDrag = 0.5f;
    public float normalAngularDrag = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearDamping = normalDrag;
                rb.angularDamping = normalAngularDrag;
            }
        }
    }
}
