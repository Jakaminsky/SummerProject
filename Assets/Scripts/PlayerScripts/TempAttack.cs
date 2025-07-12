using UnityEngine;

public class TempAttack : MonoBehaviour
{
    public float projectileSpeed = 15.0f;
    public float cooldown = 1.0f;
    public float cooldownReset = 1.0f;

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
        GameObject currentProjectile = Instantiate(projectile, spawnPoint.position, Quaternion.LookRotation(shootDirection));
        Rigidbody rb = currentProjectile.GetComponent<Rigidbody>();
        rb.linearVelocity = shootDirection * projectileSpeed;
    }


}
