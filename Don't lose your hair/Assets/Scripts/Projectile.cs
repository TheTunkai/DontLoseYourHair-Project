using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Variables
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float xBoundary = 20f;
    [SerializeField] private float lowerBoundary = -6f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Vector3 rotationVector = new Vector3(0, 0, 1f);

    #endregion

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

        this.gameObject.transform.Rotate( rotationVector* rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) // do something when colliding
    {
        if (collision.CompareTag("Enemy"))
        {
            AudioManager.instance.PlaySound(3, 1f);
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.hearts--;
            Destroy(gameObject);
        }

        if (collision.CompareTag("Obstacle") && collision.name.Contains("Obstacle_Shoot"))
        {
            AudioManager.instance.PlaySound(2, 1f);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

    }

    IEnumerator Disappear() // destroys projectile after lifetime 
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    
}
