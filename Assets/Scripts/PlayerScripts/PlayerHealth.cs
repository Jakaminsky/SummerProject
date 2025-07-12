using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float iFrames = 0.0f;
    private float iFrameDuration = 1.0f;

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
            int triggerBlock = Random.Range(1, 101);
            if(triggerBlock >= StatsManager.instance.block)
            {
                StatsManager.instance.currentHealth -= EnemyStats.damage - StatsManager.instance.armor;
            }
            iFrames = iFrameDuration;
        }
    }

    private void die()
    {
        if (StatsManager.instance.currentHealth <= 0 && StatsManager.instance.revives == 0)
        {
            foreach (MonoBehaviour script in GetComponents<MonoBehaviour>())
            {
                script.enabled = false;
            }
            Gameover.isGameover = true;
        }
        else if (StatsManager.instance.currentHealth <= 0 && StatsManager.instance.revives > 0)
        {
            StatsManager.instance.revives--;
            StatsManager.instance.currentHealth = 50;
        }
        else
        {
            Gameover.isGameover = false;
        }
    }
}
