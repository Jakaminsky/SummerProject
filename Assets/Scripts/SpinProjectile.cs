using UnityEngine;

public class SpinProjectile : MonoBehaviour
{
    public float rotationSpeed = 60.0f;

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
