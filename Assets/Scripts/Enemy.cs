using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 20;
    public GameObject deathEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        // If hit by a projectile
        if (other.CompareTag("Projectile"))
        {
            // Spawn particle effect
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }

            FindObjectOfType<ScoreManager>().AddScore(5);

            Destroy(other.gameObject);   // destroy projectile
            Destroy(gameObject);         // destroy enemy
            return;
        }

        // If hit by the player
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(damage);

            // Optional: also spawn effect when enemy hits player
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
   