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
        currentHealth = StatsManager.instance.currentHealth;
        maxHealth = StatsManager.instance.maxHealth;
    }

    private void Update()
    {
        updateHealth();
    }

    private void updateHealth()
    {
        currentHealth = Mathf.Clamp(StatsManager.instance.currentHealth, 0, StatsManager.instance.maxHealth);
        maxHealth = StatsManager.instance.maxHealth;
        
        //maxHealth = Mathf.Round(maxHealth);
        //currentHealth = Mathf.Round(currentHealth);

        healthSlider.value = currentHealth/maxHealth;
        healthText.text = Mathf.RoundToInt(currentHealth) + "/" + Mathf.RoundToInt(maxHealth);
    }
}
