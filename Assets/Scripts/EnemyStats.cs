using Unity.VisualScripting;
using UnityEngine;



public class EnemyStats : MonoBehaviour
{
    public enum type{
        normal,
        elite,
        boss
    }

    public int level = 1;
    public type enemyType;
    public float health = 50.0f;
    public float enemySpeed = 1.0f;
    public static float damage = 10.0f;

    public GameObject xpPrefab;

    private void Update()
    {
        die();
    }

    private void die()
    {
        if(health <= 0)
        {
            dropXP();
            Destroy(gameObject);
        }
    }

    private float calculateXPNum()
    {
        float multiplier = enemyType switch
        {
            type.normal => 1f,
            type.elite => 2.5f,
            type.boss => 10f,
            _ => 0f
        };

        return 5 * Mathf.Pow(level, 1.3f) * multiplier * StatsManager.instance.experienceGain;
    }

    private void dropXP()
    {
        GameObject orb = Instantiate(xpPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        orb.GetComponent<xpOrb>().xp = calculateXPNum();
    }

}

/*
Enemy XP scaling
Base XP = 5
Scale = 1.3

Multipliers
Normal = 1.0
Elite = 2.5
Boss = 10

XP dropped = base XP * EnemyLevel^Scale * rarity

Example
XP = 5 * 5^1.3 = 45
 */