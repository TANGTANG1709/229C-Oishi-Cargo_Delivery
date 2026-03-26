using UnityEngine;

public class RockDamage : MonoBehaviour
{
    public int damage = 1;
    public float knockbackForce = 500f;

    [Header("เสียงตอนหินแตก")]
    public AudioClip breakSound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            CollisionHandler hp = collision.gameObject.GetComponent<CollisionHandler>();

            if (breakSound != null)
            {
                AudioSource.PlayClipAtPoint(breakSound, transform.position);
            }

            // 💥 ใส่แรงกระแทก (เด้งออกจากหิน)
            if (rb != null)
            {
                Vector3 hitDirection = collision.contacts[0].point - transform.position;
                hitDirection = hitDirection.normalized;

                rb.AddForce(hitDirection * knockbackForce, ForceMode.Impulse);
            }

            // ❤️ ลดเลือด
            if (hp != null)
            {
                hp.TakeDamage(damage);
            }

            // 🪨 ลบหิน
            Destroy(gameObject);
        }
    }
}
