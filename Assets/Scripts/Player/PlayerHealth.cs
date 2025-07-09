using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static float maxHealth = 100.0f;
    public static float currentHealth = 100.0f;
    public float armor = 3.0f;

    private float iFrames = 0.0f;
    private float iFrameDuration = 0.2f;

    private void Update()
    {
        if(iFrames > 0)
        {
            iFrames -= Time.deltaTime;
        }
        die();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject enemy = other.gameObject;
        EnemyStats stats = enemy.GetComponent<EnemyStats>();
        if(enemy.tag == "Enemy" && iFrames <= 0)
        {
            currentHealth -= EnemyStats.damage - armor;
            iFrames = iFrameDuration;
        }
    }

    private void die()
    {
        if (currentHealth <= 0)
        {
            foreach (MonoBehaviour script in GetComponents<MonoBehaviour>())
            {
                script.enabled = false;
            }
            Gameover.isGameover = true;
        }
        else
        {
            Gameover.isGameover = false;
        }
    }
}
