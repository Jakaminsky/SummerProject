using System.Collections;
using UnityEngine;

public class BaseM2ShatteredStats : MonoBehaviour
{
    public float damage = 5f;

    private Coroutine damageCoroutine;

    private void Start()
    {
        StartCoroutine(destroyObject());
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject Enemy = collision.gameObject;
        if (Enemy.CompareTag("Enemy"))
        {
            damageCoroutine = StartCoroutine(delayDamage(Enemy));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject Enemy = collision.gameObject;
        if (Enemy.CompareTag("Enemy"))
        {
            StopCoroutine(damageCoroutine);
        }
    }

    IEnumerator delayDamage(GameObject Enemy)
    {
        for(int i = 0; i < 5; i++)
        {
            Enemy.GetComponent<EnemyStats>().health -= damage * StatsManager.instance.baseDamage;
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator destroyObject()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

}
