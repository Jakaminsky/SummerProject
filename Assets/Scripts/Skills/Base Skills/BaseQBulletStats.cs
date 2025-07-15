using UnityEngine;
using UnityEngine.AI;

public class BaseQBulletStats : MonoBehaviour
{
    public float damage = 25f;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject Enemy = collision.gameObject;
        if (Enemy.CompareTag("Enemy"))
        {
            int crit = Random.Range(1, 101);
            if(crit <= StatsManager.instance.critChance)//yes crit
            {
                Debug.Log("crit");
                Enemy.GetComponent<EnemyStats>().health -= damage * StatsManager.instance.critDamage * StatsManager.instance.baseDamage;
                Enemy.GetComponent<Knockback>().ApplyKnockback(transform.position);
                Destroy(gameObject);
            }
            else
            {
                Enemy.GetComponent<EnemyStats>().health -= damage * StatsManager.instance.baseDamage;
                Enemy.GetComponent<Knockback>().ApplyKnockback(transform.position);
                Destroy(gameObject);
            }
        }
    }
}
