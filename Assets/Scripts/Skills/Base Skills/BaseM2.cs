using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/BaseM1")]
public class BaseM2 : ActiveSkill
{
    public GameObject bulletPrefab;
    public float projectileSpeed;

    public override void Activate(GameObject user)
    {
        
    }

    //CoroutineHelper.Instance.StartCoroutine(destroyBullet(bullet));
    private IEnumerator destroyBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(bullet);
    }

}
