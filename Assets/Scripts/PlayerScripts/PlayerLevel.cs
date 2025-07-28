using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    private void Update()
    {
        suckXP();
        setNeededXP();
        levelUp();
    }

    private void setNeededXP()
    {
        float formula = 50 * Mathf.Pow(1.5f, StatsManager.instance.playerLevel) + 100;
        StatsManager.instance.neededXp = formula;
    }

    private void levelUp()
    {
        if(StatsManager.instance.currentXp >= StatsManager.instance.neededXp)
        {
            float overflowXP = StatsManager.instance.currentXp - StatsManager.instance.neededXp;
            StatsManager.instance.playerLevel++;
            StatsManager.instance.currentXp = 0 + overflowXP;
        }
    }

    private void suckXP()
    {
        GameObject[] xpOrbs = GameObject.FindGameObjectsWithTag("XP");

        foreach(GameObject orb in xpOrbs)
        {
            float distance = Vector3.Distance(transform.position, orb.transform.position);
            if (distance <= StatsManager.instance.experienceRadius)
            {
                Vector3 direction = (transform.position - orb.transform.position).normalized;
                orb.transform.position += direction * 10 * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject xp = other.gameObject;
        if (xp.CompareTag("XP"))
        {
            StatsManager.instance.currentXp += xp.GetComponent<xpOrb>().xp;
            Destroy(xp);
        }
    }

}
/*
XP needed (L) = A * L^B + C
L = current level
A = control scaling
B = exponential growth rate
C = falt base cost

Example:
Level 1 -> 2:
XP = 50 * 1^1.5 + 100 = 175

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