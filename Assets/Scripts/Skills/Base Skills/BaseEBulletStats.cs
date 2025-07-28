using System.Collections.Generic;
using UnityEngine;

public class BaseEBulletStats : MonoBehaviour
{
    public float damage = 25f;
    private HashSet <GameObject> enemiesHit = new HashSet<GameObject>();
    public int maxPierceAmount = 3;
    private int pierceCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        GameObject Enemy = other.gameObject;
        if (Enemy.CompareTag("Enemy") && !enemiesHit.Contains(Enemy))
        {
            enemiesHit.Add(Enemy);
            pierceCount++;

            int crit = Random.Range(1, 101);
            if (crit <= StatsManager.instance.critChance)//yes crit
            {
                Debug.Log("crit");
                Enemy.GetComponent<EnemyStats>().health -= damage * StatsManager.instance.critDamage * StatsManager.instance.baseDamage;
                //Destroy(gameObject);
            }
            else
            {
                Enemy.GetComponent<EnemyStats>().health -= damage * StatsManager.instance.baseDamage;
                //Destroy(gameObject);
            }

            if(pierceCount >= maxPierceAmount)
            {
                Destroy(gameObject);
            }

        }
    }

}
