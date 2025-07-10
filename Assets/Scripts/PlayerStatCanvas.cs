using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatCanvas : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthText;
    private float currentHealth;
    private float maxHealth;

    private void Start()
    {
        currentHealth = PlayerHealth.currentHealth;
        maxHealth = PlayerHealth.maxHealth;
    }

    private void Update()
    {
        updateHealth();
    }

    private void updateHealth()
    {
        currentHealth = Mathf.Clamp(PlayerHealth.currentHealth, 0, PlayerHealth.maxHealth);
        maxHealth = PlayerHealth.maxHealth;

        healthSlider.value = currentHealth/maxHealth;
        healthText.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }
}
