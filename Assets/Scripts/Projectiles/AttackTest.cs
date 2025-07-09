using UnityEngine;
using UnityEngine.UIElements;

public class AttackTest : MonoBehaviour
{
    public float projectileSpeed = 5.0f;
    public float cooldown = 1.5f;
    public float cooldownReset = 1.5f;

    public GameObject projectile;
    public Transform spawnPoint;
    private Vector3 shootDirection;

    private void Update()
    {
        getMousePosition();

        cooldown -= Time.deltaTime;
        if (cooldown < 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                attack();
                cooldown = cooldownReset;
            }
        }
    }

    private Vector3 getMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, -1.0f);
        float rayDistance;

        if(groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 worldPosition = ray.GetPoint(rayDistance);
            return worldPosition;
        }

        return spawnPoint.position;
    }

    private void attack()
    {
        Vector3 targetPosition = getMousePosition();
        shootDirection = (targetPosition - spawnPoint.position).normalized;
        GameObject currentProjectile = Instantiate(projectile, spawnPoint.position, Quaternion.LookRotation(shootDirection));
        Rigidbody rb = currentProjectile.GetComponent<Rigidbody>();
        rb.linearVelocity = shootDirection * projectileSpeed;
    }

}

/* 
 if (currentEnemy != null)
        {
            cooldown -= Time.deltaTime;
            if (cooldown < 0)
            {
                attack(currentEnemy);
                cooldown = cooldownReset;
            }
        }

GameObject[] enemies;

enemies = GameObject.FindGameObjectsWithTag("Enemy");
enemies = GameObject.FindGameObjectsWithTag("Enemy");
GameObject currentEnemy = checkClosestEnemy();

private GameObject checkClosestEnemy()
    {
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue;

            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance && distance <= range)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }



*/