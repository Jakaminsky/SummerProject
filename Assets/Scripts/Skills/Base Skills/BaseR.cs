using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Base/R")]
public class BaseR : ActiveSkill
{
    public float radius = 8f;
    public float damage = 100f;

    public override void Activate(GameObject user)
    {
        Collider[] EnemyColliders = Physics.OverlapSphere(user.transform.position, radius);
        foreach (Collider Enemy in EnemyColliders)
        {
            if (Enemy.CompareTag("Enemy") && Enemy.isTrigger == false)
            {
                int crit = Random.Range(1, 101);
                if (crit <= StatsManager.instance.critChance)//yes crit
                {
                    Debug.Log("crit");
                    Enemy.GetComponent<EnemyStats>().health -= damage * StatsManager.instance.critDamage * StatsManager.instance.baseDamage;
                    Enemy.GetComponent<Knockback>().ApplyKnockback(user.transform.position);
                }
                else
                {
                    Enemy.GetComponent<EnemyStats>().health -= damage * StatsManager.instance.baseDamage;
                    Enemy.GetComponent<Knockback>().ApplyKnockback(user.transform.position);
                }
            }
        }

        //buff by 14 % - base damage - move speed - health - cdr - size - exp gain - exp radius
        StatsManager.instance.baseDamage += 0.07f;
        StatsManager.instance.moveSpeed *= 1.07f;
        StatsManager.instance.maxHealth *= 1.07f;
        StatsManager.instance.currentHealth *= 1.07f;
        StatsManager.instance.cooldownReduction -= 0.07f;
        StatsManager.instance.projectileSize += 0.07f;
        StatsManager.instance.experienceGain += 0.07f;
        StatsManager.instance.experienceRadius *= 1.07f;
    }

}
