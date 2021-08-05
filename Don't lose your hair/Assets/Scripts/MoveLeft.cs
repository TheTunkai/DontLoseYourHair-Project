using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveLeft : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f;


    private float leftBoundary = -21f;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < leftBoundary) // destroy gameobject, if out of bounds
        {
            Destroy(gameObject);
        }

        transform.Translate(Vector2.left * speed * Time.deltaTime); // moves object to the left with assigned speed

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) // obstacle is destroyed upon collision
        {
            speed = 0;
            Destroy(gameObject);
        }
    }

}
