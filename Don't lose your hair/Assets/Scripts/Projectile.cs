using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 3f;

    public float xBoundary = 20f;
    public float lowerBoundary = -6f;

    private void Start()
    {
        StartCoroutine(Disappear());
    }

    private void Update()
    {
        if (transform.position.x < -xBoundary || transform.position.x > xBoundary || transform.position.y < lowerBoundary)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // do something when colliding
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.hearts--;
            Destroy(gameObject);
        }

    }

    IEnumerator Disappear() // destroys projectile after lifetime 
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    
}
