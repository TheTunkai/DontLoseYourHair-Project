using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public float hearts = 2;
    
    protected float speed = 22f;

    protected float leftBoundary = -21f;

    public PlayerController player;


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
