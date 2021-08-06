using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerController : MonoBehaviour
{   
    [SerializeField]
    private float jumpForce = 15f;
    private float verticalInput = 0;
    private float crouchHeight = 0.5f;
    private Vector2 scalePlayerCd = new Vector2(1f, 0.5f);

    public Vector3 target;
    public GameObject projectilePrefab;
    public float projectileSpeed = 25f;

    private bool isOnGround = true;


    private Rigidbody2D playerRb;
    private BoxCollider2D playerCd;

   public event Action heartLost;

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
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

        Vector3 difference = target - transform.position;

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

        if (Input.GetButtonDown("Fire1"))
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            Shoot(direction);
        }

    }


    private void OnCollisionEnter2D(Collision2D collision) // ground collision sets isOnGround true
    {
        if (collision.collider.CompareTag("Floor"))
        {
            isOnGround = true;
        }
        
        if (collision.collider.CompareTag("Obstacle")) // raises event upon collision
        {
            heartLost?.Invoke();

        }
    }

    public void Crouch() // scale player on y to half the height
    {
        Vector3 scale = transform.localScale;
        scale.y = crouchHeight;

        transform.localScale = scale;

        playerCd.size = scalePlayerCd;

    }

    private void Shoot(Vector2 direction) // instantiates projectile with given speed and direction of flight
    {
        GameObject projectile = Instantiate(projectilePrefab);

        projectile.transform.position = transform.position;

        projectile.GetComponent<Rigidbody2D>().velocity = projectileSpeed * direction;

    }

}
