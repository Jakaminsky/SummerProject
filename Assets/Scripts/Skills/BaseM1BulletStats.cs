using UnityEngine;

public class BaseM1BulletStats : MonoBehaviour
{
    public float damage = 25f;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject Enemy = collision.gameObject;
        if (Enemy.CompareTag("Enemy"))
        {
            int crit = Random.Range(1, 101);
            Debug.Log(crit);
            if(crit <= StatsManager.instance.critChance)//yes crit
            {
                Debug.Log("crit");
                Enemy.GetComponent<EnemyStats>().health -= damage * StatsManager.instance.critDamage;
                Destroy(gameObject);
            }
            else
            {
                Enemy.GetComponent<EnemyStats>().health -= damage;
                Destroy(gameObject);
            }
        }
    }
}
