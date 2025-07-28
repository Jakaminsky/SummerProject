using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/BaseE")]
public class BaseE : ActiveSkill
{
    public GameObject bulletPrefab;
    public float projectileSpeed;

    public override void Activate(GameObject user)
    {
        Transform spawn = user.transform.Find("SpawnPoint");
        Vector3 spawnPos = spawn != null ? spawn.position : user.transform.position;

        Vector3 dir = user.transform.forward;
        Quaternion rotation = Quaternion.LookRotation(dir);
        GameObject bullet = Instantiate(bulletPrefab, spawnPos, rotation);
        bullet.transform.localScale *= StatsManager.instance.projectileSize;
        bullet.GetComponent<Rigidbody>().linearVelocity = dir * projectileSpeed;
        CoroutineHelper.Instance.StartCoroutine(destroyBullet(bullet));
    }

    private IEnumerator destroyBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(bullet);
    }

}
