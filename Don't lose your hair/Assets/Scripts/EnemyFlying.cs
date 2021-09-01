using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyFlying : Enemy
{
    #region Variables
    [SerializeField] private int suckInterval = 4;
    [SerializeField] private Vector2 endPosition = new Vector2(-8f, 3f);
    [SerializeField] private float suckSpeed = 0.3f;

    [SerializeField] private bool suckingStarted = false;
    public Animator enemyAnimator;

    
    
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.playerLost += OnGameOver;
        GameManager.instance.gamePaused += OnPause;
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (transform.position.x <= endPosition.x)
        {
            

            if(!suckingStarted) // when flying enemy starts sucking for first time, it starts playing the sucking sound and changes its animation
            {
                enemyAnimator.SetBool("at_final_position", true);
                AudioManager.instance.PlaySound(3, 1f, true);
                suckingStarted = true;
            }
            

            if (!GameManager.instance.gameOver && UIManager.instance.plushReserve >= 0) // when player has plush left and the game is not over, the enemy sucks its hair
            {
                SuckHair();
            }
            else if (!GameManager.instance.gameOver)    // if the player has no plush left, his hearts will decrease
            {
                StartCoroutine(DecreasePlayerHearts());
            }
            
        }

        if (hearts <= 0)
        {
            Die();
        }

        

    }

    public override void Move() // extends base method move 
    {
        if (transform.position.x <= endPosition.x) // movement stops at end position
        {
            this.speed = 0;
        }

        base.Move();
    }

    void Die() // game object is destroyed and sounds are stopped
    {
        AudioManager.instance.PlaySound(5, 1f, false);
        AudioManager.instance.StopSoundLoop();
        Destroy(gameObject);
    }

    void SuckHair() // decreases players plush reserve over time
    {
        UIManager.instance.plushReserve -= suckSpeed * Time.deltaTime;
    }

    void OnGameOver()
    {
        AudioManager.instance.StopSoundLoop();
    }

    void OnPause()
    {
        if (suckingStarted)
        {
            suckingStarted = false;
        }
        
        AudioManager.instance.StopSoundLoop();

    }

    IEnumerator DecreasePlayerHearts() // decreases players hearts with given interval
    {
        while (!GameManager.instance.gameOver && UIManager.instance.plushReserve <= 0)
        {

            yield return new WaitForSeconds(suckInterval);
            GameManager.instance.playerHearts--;
  
        }
            

    }

}
