using UnityEngine;

public class TempBulletStats : MonoBehaviour
{
    public float damage = 25f;
    public float size = 1.0f;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject Enemy = collision.gameObject;
        if(Enemy.CompareTag("Enemy"))
        {
            Enemy.GetComponent<EnemyStats>().health -= damage;
            Destroy(gameObject);
        }
    }
}
