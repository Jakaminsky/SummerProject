using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    public float moveSpeed = 7.5f;
    public float dashSpeed = 20.0f;
    public float dashCooldown = 3.0f;
    public float dashDuration = 0.2f;
    public int numberOfDashes = 2;

    private float lastDashTime;
    private Vector3 moveDirection;
    private bool isDashing;

    [Header("Canvas Variables")]
    public TMP_Text currentDashes;
    public Image dashIcon;

    private void Start()
    {
        lastDashTime = dashCooldown;
    }

    private void Update()
    {
        currentDashes.text = numberOfDashes.ToString();

        if (!isDashing)
        {
            movePlayer();
        }

        lastDashTime -= Time.deltaTime;
        if(lastDashTime < 0)
        {
            numberOfDashes = 2;
            lastDashTime = dashCooldown;
        }

        dashIcon.fillAmount = 1f - (lastDashTime / 3f);

        if (Input.GetKeyDown(KeyCode.Space) && numberOfDashes > 0 && moveDirection != Vector3.zero)
        {
            StartCoroutine(dash());
            numberOfDashes--;
        }
    }

    private void movePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    private IEnumerator dash()
    {
        isDashing = true;

        float elapsedTime = 0.0f;

        gameObject.GetComponent<CapsuleCollider>().enabled = false;

        while (elapsedTime < dashDuration)
        {
            transform.Translate(moveDirection * dashSpeed * Time.deltaTime, Space.World);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.GetComponent<CapsuleCollider>().enabled = true;

        isDashing = false;
    }

}
