using UnityEngine;

public class RockDamage : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollisionHandler hp = other.GetComponent<CollisionHandler>();

            if (hp != null)
                hp.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
