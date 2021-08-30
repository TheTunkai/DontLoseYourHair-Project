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
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (transform.position.x <= endPosition.x)
        {
            

            if(!suckingStarted)
            {
                enemyAnimator.SetBool("at_final_position", true);
                AudioManager.instance.PlaySound(3, 1f, true);
                suckingStarted = true;
            }
            

            if (!GameManager.instance.gameOver && UIManager.instance.plushReserve >= 0)
            {
                SuckHair();
            }
            else if (!GameManager.instance.gameOver)
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
        if (transform.position.x <= endPosition.x)
        {
            this.speed = 0;
        }

        base.Move();
    }

    void Die()
    {
        AudioManager.instance.PlaySound(5, 1f, false);
        AudioManager.instance.StopSoundLoop();
        Destroy(gameObject);
    }

    void SuckHair()
    {
        UIManager.instance.plushReserve -= suckSpeed * Time.deltaTime;
    }

    IEnumerator DecreasePlayerHearts() // starts sucking player life (hearts, later hair) with given interval
    {
        while (!GameManager.instance.gameOver && UIManager.instance.plushReserve <= 0)
        {

            yield return new WaitForSeconds(suckInterval);
            GameManager.instance.playerHearts--;
  
        }
            

    }

}
