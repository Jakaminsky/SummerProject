using UnityEngine;

//public enum OrbitDamageType
//{
//    Normal,
//    Fire,
//    Ice,
//    Poison,
//    Explosive,
//}

public class OrbitElements : MonoBehaviour
{
    public DamageType orbitdamageType = DamageType.Normal;

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Projectile")
        {
            GameObject projectile = other.gameObject;
            projectile.AddComponent<ProjectileDamage>().damageType = orbitdamageType;
        }
    }
}
