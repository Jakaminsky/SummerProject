using Unity.VisualScripting;
using UnityEngine;

public class BaseM2BottleStats : MonoBehaviour
{
    public float damage = 30f;
    public float radius = 3f;
    private bool hasExploded = false;

    public GameObject shatteredGlass;

    private GameObject ground;

    private void Start()
    {
        ground = GameObject.Find("Ground");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasExploded)
        {
            return;
        }

        hasExploded = true;

        Collider[] EnemyColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider Enemy in EnemyColliders){
            if (Enemy.CompareTag("Enemy") && Enemy.isTrigger == false)
            {
                int crit = Random.Range(1, 101);
                if (crit <= StatsManager.instance.critChance)//yes crit
                {
                    Debug.Log("crit");
                    Enemy.GetComponent<EnemyStats>().health -= damage * StatsManager.instance.critDamage * StatsManager.instance.baseDamage;
                }
                else
                {
                    Enemy.GetComponent<EnemyStats>().health -= damage * StatsManager.instance.baseDamage;
                }
            }
        }
        Instantiate(shatteredGlass, new Vector3 (gameObject.transform.position.x, ground.transform.position.y + 0.5f, gameObject.transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
