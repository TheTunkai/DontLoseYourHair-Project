using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    #region Variables
    public float hearts = 1;
   [SerializeField] protected float speed = 22f;
   [SerializeField] protected float leftBoundary = -21f;

    private PlayerController player;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

    }
    
    // Update is called once per frame
    void Update()
    {
        

        
    }


    public virtual void Move() // moves enemies
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < leftBoundary)
        {
            Destroy(gameObject);
        }

    }
}
