using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackEffects : MonoBehaviour
{
    EnemyStats stats;

    public ParticleSystem fireVFXPrefab;
    public ParticleSystem firefireVFXPrefab;
    public ParticleSystem iceVFXPrefab;
    public ParticleSystem iceiceVFXPrefab;
    public ParticleSystem poisonVFXPrefab;
    public ParticleSystem explosionVFXPrefab;

    private Dictionary<string, Action> elementCombos;

    private void Start()
    {
        stats = GetComponent<EnemyStats>();

        elementCombos = new Dictionary<string, Action>
        {
            {"Fire+Fire", ApplyDoubleFire},
            {"Fire+Ice", ApplyFireIce},
            {"Fire+Poison", ApplyFirePoison},
            {"Explosive+Fire", ApplyFireExplosive},
            {"Ice+Ice", ApplyDoubleIce},
            {"Ice+Poison", ApplyIcePoison},
            {"Explosive+Ice", ApplyIceExplosive},
            {"Poison+Poison", ApplyDoublePoison},
            {"Explosive+Poison", ApplyPoisonExplosive},
            {"Explosive+Explosive", DoubleExplode}
        };
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            ProjectileDamage[] projectiles = collision.gameObject.GetComponents<ProjectileDamage>();
            List<string> typeStrings = projectiles.Select(p => p.damageType.ToString()).ToList();
            typeStrings.Sort();
            string comboKey = string.Join("+", typeStrings);

            stats.health -= projectiles[0].damage;

            if (elementCombos.TryGetValue(comboKey, out Action comboEffect))
            {
                comboEffect();
            }
            else
            {
                foreach (var p in projectiles)
                {
                    switch (p.damageType)
                    {
                        case DamageType.Fire:
                            StartCoroutine(ApplyFire());
                            break;
                        case DamageType.Ice:
                            StartCoroutine(ApplyIce(p));
                            break;
                        case DamageType.Poison:
                            StartCoroutine(ApplyPoison());
                            break;
                        case DamageType.Explosive:
                            Explode(p);
                            break;
                    }
                }
            }
            Destroy(collision.gameObject);
        }
    }

    //When balancing change particle details using scripts
    private IEnumerator ApplyFire()
    {
        ParticleSystem fireVFX = Instantiate(fireVFXPrefab, transform.position, Quaternion.identity);
        fireVFX.transform.parent = gameObject.transform;
        int ticks = 5;
        for (int i = 0; i < ticks; i++)
        {
            stats.health -= 2f;
            yield return new WaitForSeconds(1f);
        }
        Destroy(fireVFX.gameObject);
    }

    bool isOnFire = false;
    private IEnumerator ApplyModifiedFire()
    {
        if (isOnFire) { yield break; }
        isOnFire = true;

        ParticleSystem fireVFX = Instantiate(fireVFXPrefab, transform.position, Quaternion.identity);
        fireVFX.transform.parent = gameObject.transform;
        int ticks = 5;
        float damage = 2f;
        for (int i = 0; i < ticks; i++)
        {
            stats.health -= damage;
            if(stats.health <= 0f)
            {
                SpreadFire();
            }
            yield return new WaitForSeconds(1f);
        }
        Destroy(fireVFX.gameObject);
        isOnFire = false;
    }

    private void SpreadFire()
    {
        float spreadRadius = 3.0f;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, spreadRadius);
        foreach (Collider collider in hitColliders)
        {
            AttackEffects spread = collider.GetComponent<AttackEffects>();
            if (collider.tag == "Enemy" && collider.gameObject != gameObject)
            {
                if (spread != null)
                {
                    spread.StartCoroutine(spread.ApplyModifiedFire());
                }
            }
        }
    }

    private void ApplyDoubleFire()
    {
        StartCoroutine(DoubleFire());
    }

    private IEnumerator DoubleFire()
    {
        ParticleSystem fireVFX = Instantiate(firefireVFXPrefab, transform.position, Quaternion.identity);
        fireVFX.transform.parent = gameObject.transform;
        int ticks = 6;
        float damage = 2f;
        for (int i = 0; i < ticks; i++)
        {
            damage = Mathf.Pow(damage, 1.25f);
            stats.health -= damage;
            yield return new WaitForSeconds(1f);
        }
        Destroy(fireVFX.gameObject);
    }

    private void ApplyFireIce()//not done
    {
        //steam cloud
        Debug.Log("FIRE ICE");
    }

    private void ApplyFirePoison()
    {
        StartCoroutine(FirePoison());
    }

    private IEnumerator FirePoison()
    {
        StartCoroutine(ApplyModifiedFire());
        StartCoroutine(ApplyPoison());
        yield break;
    }

    private void ApplyFireExplosive()//not done
    {
        //fire explosive
        Debug.Log("FIRE EXPLOSIVE");
    }

    private IEnumerator ApplyIce(ProjectileDamage projectile)
    {
        ParticleSystem iceVFX = Instantiate(iceVFXPrefab, transform.position, Quaternion.identity);
        iceVFX.transform.parent = gameObject.transform;
        float originalSpeed = stats.enemySpeed;
        stats.enemySpeed = stats.enemySpeed * 0.75f;
        yield return new WaitForSeconds(2f);
        stats.enemySpeed = originalSpeed;
        Destroy(iceVFX.gameObject);
    }

    bool isIced = false;
    private IEnumerator ApplyModifiedIce()
    {
        if (isIced) { yield break; }
        isIced = true;

        ParticleSystem iceVFX = Instantiate(iceVFXPrefab, transform.position, Quaternion.identity);
        iceVFX.transform.parent = gameObject.transform;
        float originalSpeed = stats.enemySpeed;
        stats.enemySpeed = stats.enemySpeed * 0.75f;
        stats.health -= 0.001f;
        if (stats.health <= 0)
        {
            SpreadIce();
        }
        yield return new WaitForSeconds(2f);
        stats.enemySpeed = originalSpeed;
        
        Destroy(iceVFX.gameObject);
        isIced = false;
    }

    private void SpreadIce()
    {
        float spreadRadius = 3.0f;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, spreadRadius);
        foreach (Collider collider in hitColliders)
        {
            AttackEffects spread = collider.GetComponent<AttackEffects>();
            if (collider.tag == "Enemy" && collider.gameObject != gameObject)
            {
                if (spread != null)
                {
                    spread.StartCoroutine(spread.ApplyModifiedIce());
                }
            }
        }
    }

    private void ApplyDoubleIce()
    {
        StartCoroutine(DoubleIce());
    }

    private IEnumerator DoubleIce()
    {
        ParticleSystem iceVFX = Instantiate(iceiceVFXPrefab, transform.position, Quaternion.identity);
        iceVFX.transform.parent = gameObject.transform;
        float originalSpeed = stats.enemySpeed;
        stats.enemySpeed = 0;
        yield return new WaitForSeconds(4f);
        stats.enemySpeed = originalSpeed;
        Destroy(iceVFX.gameObject);
    }

    private void ApplyIcePoison()
    {
        StartCoroutine(IcePoison());
    }

    private IEnumerator IcePoison()
    {
        StartCoroutine(ApplyPoison());
        StartCoroutine(ApplyModifiedIce());
        yield break;
    }

    private void ApplyIceExplosive()//not done
    {
        //ice shards
        Debug.Log("ICE EXPLOSIVE");
    }

    bool isPoisoned = false;
    private IEnumerator ApplyPoison()
    {
        if(isPoisoned) { yield break; }
        isPoisoned = true;

        Quaternion newRotation = Quaternion.Euler(90, 0, 0);
        Vector3 newPosition = new Vector3(transform.position.x, 2.6f, transform.position.z);
        ParticleSystem poisonVFX = Instantiate(poisonVFXPrefab, newPosition, newRotation);
        poisonVFX.transform.parent = gameObject.transform;
        int ticks = 8;
        float damage = 1;
        for (int i = 0; i < ticks; i++)
        {
            if (isPoisoned)
            {
                damage += 0.25f;
            }
            stats.health -= damage;
            if(stats.health <= 0f)
            {
                SpreadPoison();
            }
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(poisonVFX.gameObject);
        isPoisoned = false;
    }

    private void SpreadPoison()
    {
        float spreadRadius = 3.0f;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, spreadRadius);
        foreach (Collider collider in hitColliders)
        {
            AttackEffects spread = collider.GetComponent<AttackEffects>();
            if(collider.tag == "Enemy" && collider.gameObject != gameObject)
            {
                if(spread != null)
                {
                    spread.StartCoroutine(spread.ApplyPoison());
                }
            }
        }
    }

    private void ApplyDoublePoison()
    {
        StartCoroutine(DoublePoison());
    }

    private IEnumerator DoublePoison()
    {
        if (isPoisoned) { yield break; }
        isPoisoned = true;

        Quaternion newRotation = Quaternion.Euler(90, 0, 0);
        Vector3 newPosition = new Vector3(transform.position.x, 2.6f, transform.position.z);
        ParticleSystem poisonVFX = Instantiate(poisonVFXPrefab, newPosition, newRotation);
        poisonVFX.transform.parent = gameObject.transform;
        int ticks = 8;
        float damage = 1;
        for (int i = 0; i < ticks; i++)
        {
            if (isPoisoned)
            {
                damage += 1f;
            }
            stats.health -= damage;
            SpreadPoison();
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(poisonVFX.gameObject);
        isPoisoned = false;
    }

    private void ApplyPoisonExplosive()//not done
    {
        //poison cloud
        Debug.Log("POISON EXPLOSIVE");
    }

    private void Explode(ProjectileDamage projectile)//hits X times to enemies with X colliders
    {
        float explosionRadius = 5.0f;
        float explosionDamage = (projectile.damage / 4);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject == this.gameObject)
            {
                continue;
            }
            if (hitCollider.tag == "Enemy")
            {
                EnemyStats tempStats = hitCollider.GetComponent<EnemyStats>();
                if (tempStats != null)
                {
                    tempStats.health -= explosionDamage;
                }
            }
        }

        ParticleSystem explosionVFX = Instantiate(explosionVFXPrefab, transform.position, Quaternion.identity);
        Destroy(explosionVFX.gameObject, 2f);
    }

    private void DoubleExplode()//not done
    {
        //double explode
        Debug.Log("DOUBLE EXPLODE");
    }

}