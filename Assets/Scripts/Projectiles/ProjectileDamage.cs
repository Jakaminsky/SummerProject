using UnityEngine;

public enum DamageType
{
    Normal,
    Fire,
    Ice,
    Poison,
    Explosive,
}

public class ProjectileDamage : MonoBehaviour
{
    public float damage = 5.0f;
    public DamageType damageType = DamageType.Normal;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
