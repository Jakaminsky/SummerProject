using UnityEngine;

public class AerialAttackTest : MonoBehaviour
{
    public float projectileSpeed = 5.0f;
    public float cooldown = 5.0f;
    public float cooldownReset = 5.0f;

    public GameObject projectile;
    public Transform spawnPoint;
    private Vector3 shootDirection;

    private void Update()
    {
        getMousePosition();

        cooldown -= Time.deltaTime;
        if (cooldown < 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                attack();
                cooldown = cooldownReset;
            }
        }
    }

    private Vector3 getMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, -2f);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
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

        float horizontalDistance = new Vector3(targetPosition.x - spawnPoint.position.x, 0, targetPosition.z - spawnPoint.position.z).magnitude;
        float lobAngle = 45.0f;
        float verticalVelocity = Mathf.Tan(lobAngle * Mathf.Deg2Rad) * horizontalDistance;

        Vector3 velocity = shootDirection * projectileSpeed;
        velocity.y = verticalVelocity;

        GameObject currentProjectile = Instantiate(projectile, spawnPoint.position, Quaternion.LookRotation(shootDirection));
        Rigidbody rb = currentProjectile.GetComponent<Rigidbody>();
        rb.linearVelocity = velocity;
    }

}

/*
 *  private GameObject checkClosestEnemy()
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