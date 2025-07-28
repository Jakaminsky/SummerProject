using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Base/Q")]
public class BaseQ : ActiveSkill
{
    public GameObject bulletPrefab;
    public float projectileSpeed;
    public int bulletCount;
    public float totalSpreadAngle = 30f;

    public override void Activate(GameObject user)
    {
        Transform spawn = user.transform.Find("SpawnPoint");
        Vector3 spawnPos = spawn != null ? spawn.position : user.transform.position;

        float angleStep = bulletCount > 1 ? totalSpreadAngle / (bulletCount - 1) : 0f;
        float startAngle = -totalSpreadAngle / 2f;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + i * angleStep;

            Vector3 direction = Quaternion.Euler(0f, angle, 0f) * user.transform.forward;

            GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.LookRotation(direction));
            CoroutineHelper.Instance.StartCoroutine(destroyBullet(bullet));

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = direction.normalized * projectileSpeed;
            }
        }
    }

    private IEnumerator destroyBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(0.15f);
        Destroy(bullet);
    }

}
