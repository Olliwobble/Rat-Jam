using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI Slider Reference")]
    public Slider healthSlider;
    public float smoothSpeed = 5f; // How fast the slider catches up to the real health value

    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }

    void Update()
    {
        // Smoothly slide the UI bar to match current structural health over time
        if (healthSlider != null && !Mathf.Approximately(healthSlider.value, currentHealth))
        {
            healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth, smoothSpeed * Time.deltaTime);
        }
    }

    // Call this public function from your Turn Manager when the enemy acts!
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Defeated!");
        // Trigger your Turn Manager's Loss state here
    }
}
