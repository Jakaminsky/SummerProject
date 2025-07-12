using Unity.VisualScripting;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float health = 50.0f;
    public float enemySpeed = 1.0f;
    public static float damage = 10.0f;

    private void Update()
    {
        die();
    }

    private void die()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
