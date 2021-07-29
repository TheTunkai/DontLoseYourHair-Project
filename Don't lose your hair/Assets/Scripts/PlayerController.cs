using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [SerializeField]
    private float speed = 18f;
    [SerializeField]
    private float jumpForce = 15f;
    private float dashSpeed = 30f;
    private float horizontalInput = 0;
    private float verticalInput = 0;
    private float crouchHeight = 0.5f;
    private Vector2 scalePlayerCd = new Vector2(1f, 0.5f);


    private bool isOnGround = true;
    private bool isDashing = false;
    private bool canDash = true;


    private Rigidbody2D playerRb;
    private BoxCollider2D playerCd;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerCd = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (canDash && Input.GetKeyDown(KeyCode.E)) // check for dash input
        {
            isDashing = true;
            StartCoroutine(DashCountdown());
        }

        if (!isDashing) // depending on whether player dasher or not the translation is slower or faster
        {
            transform.Translate(Vector2.right * speed * horizontalInput * Time.deltaTime);
        }
        else if (isDashing)
        {
            transform.Translate(Vector2.right * dashSpeed * horizontalInput * Time.deltaTime);
        }
        

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround) // make player jump if he is on the ground
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
        }

        if (verticalInput < 0) // player crouches with negative Input on the vertical axis
        {
            Crouch();
        }
        else
        {
            transform.localScale = Vector3.one;
            playerCd.size = Vector2.one;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision) // ground collision sets isOnGround true
    {
        if (collision.collider.CompareTag("Floor"))
        {
            isOnGround = true;
        }
    }

    public void Crouch() // scale player on y to half the height
    {
        Vector3 scale = transform.localScale;
        scale.y = crouchHeight;

        transform.localScale = scale;

        playerCd.size = scalePlayerCd;

    }

    IEnumerator DashCountdown() // counts down the dash time
    {
        yield return new WaitForSeconds(2);
        isDashing = false;
        canDash = false;

        StartCoroutine(DashRecovery());
    }

    IEnumerator DashRecovery() // recovery time for dashing is waited until player can dash again
    {
        yield return new WaitForSeconds(5);
        canDash = true;
    }

}
