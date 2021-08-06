using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : Enemy
{
    
    public int suckInterval = 4;

    public Vector2 endPosition = new Vector2(-9f, 3f);

    public bool suckingStarted = false;

  


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (transform.position.x <= endPosition.x && !suckingStarted)
        {
            suckingStarted = true;
            StartCoroutine(SuckHair());
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
        Destroy(gameObject);
    }

    IEnumerator SuckHair() // starts sucking enemy life (hearts, later hair) with given interval
    {
        while (GameManager.instance.playerHearts > 0)
        {

            yield return new WaitForSeconds(suckInterval);
            GameManager.instance.playerHearts--;
  
        }
            

    }

}
