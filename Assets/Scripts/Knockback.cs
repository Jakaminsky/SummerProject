using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Knockback : MonoBehaviour
{
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.3f;

    private Rigidbody rb;
    private bool isKnockedBack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void ApplyKnockback(Vector3 sourcePosition)
    {
        if (isKnockedBack) return;

        isKnockedBack = true;

        Vector3 direction = (transform.position - sourcePosition).normalized;
        direction.y = 0f;

        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        rb.isKinematic = false;

        rb.linearVelocity = Vector3.zero;
        rb.AddForce(direction * knockbackForce, ForceMode.Impulse);

        StartCoroutine(RecoverFromKnockback());
    }

    private IEnumerator RecoverFromKnockback()
    {
        yield return new WaitForSeconds(knockbackDuration);
        rb.linearVelocity = Vector3.zero;
        isKnockedBack = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        rb.isKinematic = true;
    }
}
