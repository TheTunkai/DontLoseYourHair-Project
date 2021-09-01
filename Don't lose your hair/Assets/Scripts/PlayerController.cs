using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float verticalInput = 0;
    [SerializeField] private Vector2 normalCdSize = new Vector2(1.8f, 1.9f);
    [SerializeField] private Vector2 sizePlayerCdCrouch = new Vector2(1.8f, 0.5f);
    [SerializeField] private Vector3 target;
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private float jumpCost = 0.1f;
    [SerializeField] private float crouchCost = 0.5f;
    [SerializeField] private float shootCost = 0.2f;
    [SerializeField] private float collisionCost = 0.4f;

    [SerializeField] private bool isOnGround = true;

    public GameObject projectilePrefab;
    private Rigidbody2D playerRb;
    private BoxCollider2D playerCd;
    public Animator playerAnimator;

    public ParticleSystem jumpParticleBurst;
    #endregion

    #region Events
    public event Action heartLost;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerCd = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameIsPaused && !GameManager.instance.gameOver)
        {
            verticalInput = Input.GetAxis("Vertical");
            target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

            Vector3 difference = target - transform.position;

            if (Input.GetKeyDown(KeyCode.Space) && isOnGround && UIManager.instance.plushReserve - jumpCost >= 0) // make player jump if he is on the ground and play sound
            {
                jumpParticleBurst.Play();
                playerAnimator.SetBool("is_jumping_b", true);
                AudioManager.instance.PlaySound(0, 0.6f, false);
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                UIManager.instance.plushReserve -= jumpCost;
                isOnGround = false;
            }

            if (verticalInput < 0) // player crouches with negative Input on the vertical axis
            {
                Crouch();
            }
            else
            {
                playerAnimator.SetBool("is_crouching_b", false);
                playerCd.size = normalCdSize;
                
            }

            if (Input.GetButtonDown("Fire1") && UIManager.instance.plushReserve - shootCost >= 0) // player shoots as long as enough plush is in reserve
            {
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                Shoot(direction);
            }
        }

        if (GameManager.instance.gameOver) // disables animator when game is over
        {
            playerAnimator.enabled = false;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision) // ground collision sets isOnGround true
    {
        if (collision.collider.CompareTag("Floor"))
        {
            playerAnimator.SetBool("is_jumping_b", false);
            isOnGround = true;
        }
        
        if (collision.collider.CompareTag("Obstacle")) // raises event upon collision and plays sound
        {
            if (UIManager.instance.plushReserve - collisionCost > 0)
            {
                AudioManager.instance.PlaySound(1, 0.6f, false);
                UIManager.instance.plushReserve -= collisionCost;
            }
            else
            {
                AudioManager.instance.PlaySound(1, 0.6f, false);
                UIManager.instance.plushReserve = 0;
            }
            
            heartLost?.Invoke();

        }
    }

    public void Crouch() // scale player on y to half the height and substract cost from plush reserve
    {
        if (UIManager.instance.plushReserve - crouchCost * Time.deltaTime >= 0)
        {
            playerAnimator.SetBool("is_crouching_b", true);

            playerCd.size = sizePlayerCdCrouch;

            UIManager.instance.plushReserve -= crouchCost * Time.deltaTime;
        }
        else 
        { 
            return; 
        }
    }

    private void Shoot(Vector2 direction) // instantiates projectile with given speed and direction of flight and plays shoot sound
    {
        AudioManager.instance.PlaySound(6, 1f, false);
        
        GameObject projectile = Instantiate(projectilePrefab);

        projectile.transform.position = transform.position;

        projectile.GetComponent<Rigidbody2D>().velocity = projectileSpeed * direction;

        UIManager.instance.plushReserve -= shootCost;
       
    }

}
