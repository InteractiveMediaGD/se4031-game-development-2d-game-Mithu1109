using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    public CameraShake cameraShake;
    public TextMeshProUGUI healthText;
    public DamageFlash damageFlash;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    void Update()
    {
        // Restart game when R is pressed
        if (currentHealth <= 0 && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f; // reset time
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateUI();

        Debug.Log("Player Took Damage!");

        if (cameraShake != null)
            cameraShake.Shake(0.2f, 0.2f);

        if (damageFlash != null)
            damageFlash.Flash();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            UpdateUI();

            if (cameraShake != null)
                cameraShake.StopShake();   // IMPORTANT FIX

            Time.timeScale = 0f;
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateUI();
    }

    public void UpdateUI()
{
    healthText.text = "Health: " + currentHealth;

    float healthPercent = (float)currentHealth / maxHealth;

    // Smooth gradient from red (low) to green (full)
    Color healthColor = Color.Lerp(Color.red, Color.green, healthPercent);

    healthText.color = healthColor;
}
void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Wall"))
    {
        TakeDamage(1);   // adjust damage value if needed
    }
}
}