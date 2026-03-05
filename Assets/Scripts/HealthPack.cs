using UnityEngine;
using System.Collections;

public class HealthPack : MonoBehaviour
{
    public int healAmount = 1;
    public float popDuration = 0.2f;
    public float popScaleMultiplier = 1.3f;

    private Collider2D col;
    private AudioSource audioSource;

    void Start()
    {
        col = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.Heal(healAmount);
            StartCoroutine(PopAndDestroy());
        }
    }

    IEnumerator PopAndDestroy()
    {
        // Prevent double collection
        col.enabled = false;

        // Play collect sound
        if (audioSource != null)
            audioSource.Play();

        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * popScaleMultiplier;

        float time = 0f;

        while (time < popDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, time / popDuration);
            time += Time.deltaTime;
            yield return null;
        }

        // Wait for sound to finish before destroying
        if (audioSource != null && audioSource.clip != null)
            yield return new WaitForSeconds(audioSource.clip.length);

        Destroy(gameObject);
    }
}