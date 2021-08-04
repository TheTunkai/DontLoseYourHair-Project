using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void Notify();

public class PlayerController : MonoBehaviour
{   
    [SerializeField]
    private float jumpForce = 15f;
    private float verticalInput = 0;
    private float crouchHeight = 0.5f;
    private Vector2 scalePlayerCd = new Vector2(1f, 0.5f);


    private bool isOnGround = true;


    private Rigidbody2D playerRb;
    private BoxCollider2D playerCd;

    public event Notify heartLost;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerCd = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");

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
        
        if (collision.collider.CompareTag("Obstacle"))
        {
            if (heartLost != null)
            {
                heartLost.Invoke();
            }
            
        }
    }

    public void Crouch() // scale player on y to half the height
    {
        Vector3 scale = transform.localScale;
        scale.y = crouchHeight;

        transform.localScale = scale;

        playerCd.size = scalePlayerCd;

    }

}
