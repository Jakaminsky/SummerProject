using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Base/M1")]
public class BaseM1 : ActiveSkill
{
    public GameObject bulletPrefab;
    public float projectileSpeed;

    public ParticleSystem muzzleFlash;

    public override void Activate(GameObject user)
    {
        Transform spawn = user.transform.Find("SpawnPoint");
        Vector3 spawnPos = spawn != null ? spawn.position : user.transform.position;

        ParticleSystem flash = Instantiate(muzzleFlash, spawnPos, Quaternion.identity);

        Vector3 dir = user.transform.forward;
        Quaternion rotation = Quaternion.LookRotation(dir);
        GameObject bullet = Instantiate(bulletPrefab, spawnPos, rotation);
        bullet.transform.localScale *= StatsManager.instance.projectileSize;
                
        bullet.GetComponent<Rigidbody>().linearVelocity = (dir * projectileSpeed);
        CoroutineHelper.Instance.StartCoroutine(destroyBullet(bullet, flash));
    }

    private IEnumerator destroyBullet(GameObject bullet, ParticleSystem muzzleFlash)
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(bullet);
        Destroy(muzzleFlash.gameObject);
    }

}
