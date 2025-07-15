using System.Collections;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/BaseM2")]
public class BaseM2 : ActiveSkill
{
    public GameObject bottlePrefab;
    public float launchAngle;

    Camera cam;

    public override void Activate(GameObject user)
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        Transform spawn = user.transform.Find("SpawnPoint");
        Vector3 spawnPos = spawn != null ? spawn.position : user.transform.position;

        ThrowProjectile(spawnPos);
    }

    void ThrowProjectile(Vector3 spawnPos)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 target = hit.point;
            GameObject projectile = Instantiate(bottlePrefab, spawnPos, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            Vector3 velocity = CalculateArcVelocity(target, spawnPos, launchAngle);
            rb.linearVelocity = velocity;
        }
    }

    Vector3 CalculateArcVelocity(Vector3 target, Vector3 start, float height)
    {
        Vector3 displacement = target - start;
        Vector3 displacementXZ = new Vector3(displacement.x, 0, displacement.z);

        float timeToApex = Mathf.Sqrt(2 * height / -Physics.gravity.y);
        float totalTime = timeToApex + Mathf.Sqrt(2 * (displacement.y - height) / -Physics.gravity.y);

        if (float.IsNaN(totalTime)) totalTime = timeToApex * 2; // fallback if target is on same level or above

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(2 * -Physics.gravity.y * height);
        Vector3 velocityXZ = displacementXZ / totalTime;

        return velocityXZ + velocityY;
    }

}
