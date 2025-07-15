using System.Collections;
using UnityEngine;

public class BaseM2ShatteredStats : MonoBehaviour
{
    public float damage = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject Enemy = collision.gameObject;
        if (Enemy.CompareTag("Enemy"))
        {
           StartCoroutine(delayDamage(Enemy));
        }
    }

    IEnumerator delayDamage(GameObject Enemy)
    {
        for(int i = 0; i < 5; i++)
        {
            Enemy.GetComponent<EnemyStats>().health -= damage * StatsManager.instance.baseDamage;
            yield return new WaitForSeconds(1);
        }
        Destroy(gameObject);
    }

}
