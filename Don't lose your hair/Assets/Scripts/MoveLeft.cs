using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveLeft : MonoBehaviour
{
    #region Variables
    [SerializeField] private float speed = 20f;
    [SerializeField] private float leftBoundary = -21f;
    [SerializeField] private Vector3 startPosBackground = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 endPosBackground = new Vector3(-32f, 0f, -1f);
    [SerializeField] private Vector3 startPosFloor = new Vector3(-0.73f, -2.35f, -1f);
    [SerializeField] private Vector3 endPosFloor = new Vector3(-35.42f, -2.35f, -1f);
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameOver)
        {

            if (transform.position.x < leftBoundary && gameObject.CompareTag("Obstacle")) // destroy gameobject, if out of bounds
            {
                Destroy(gameObject);
            }

            if (transform.position.x <= endPosBackground.x && gameObject.CompareTag("Background"))
            {
                transform.position = startPosBackground;
            }

            if (transform.position.x <= endPosFloor.x && gameObject.CompareTag("Floor"))
            {
                transform.position = startPosFloor;
            }

            transform.Translate(Vector2.left * speed * Time.deltaTime); // moves object to the left with assigned speed
        }
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
