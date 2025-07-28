using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    private float lastDashTime;
    private Vector3 moveDirection;
    private bool isDashing;

    [Header("Canvas Variables")]
    public TMP_Text currentDashes;
    public Image dashIcon;

    private void Start()
    {
        lastDashTime = StatsManager.instance.dashCooldown;
    }

    private void Update()
    {
        currentDashes.text = StatsManager.instance.numberOfDashes.ToString();

        if (!isDashing)
        {
            movePlayer();
        }

        lastDashTime -= Time.deltaTime;
        if(lastDashTime < 0)
        {
            StatsManager.instance.numberOfDashes = StatsManager.instance.totalDashes;
            lastDashTime = StatsManager.instance.dashCooldown;
        }

        dashIcon.fillAmount = 1f - (lastDashTime / StatsManager.instance.dashCooldown);

        if (Input.GetKeyDown(KeyCode.Space) && StatsManager.instance.numberOfDashes > 0 && moveDirection != Vector3.zero)
        {
            StartCoroutine(dash());
            StatsManager.instance.numberOfDashes--;
        }
    }

    private void movePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
        transform.Translate(moveDirection * StatsManager.instance.moveSpeed * Time.deltaTime, Space.World);
    }

    private IEnumerator dash()
    {
        isDashing = true;

        float elapsedTime = 0.0f;

        gameObject.GetComponent<CapsuleCollider>().enabled = false;

        while (elapsedTime < StatsManager.instance.dashDuration)
        {
            transform.Translate(moveDirection * StatsManager.instance.dashSpeed * Time.deltaTime, Space.World);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.GetComponent<CapsuleCollider>().enabled = true;

        isDashing = false;
    }

}
