using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyScript : MonoBehaviour
{
    private Transform playerTransform;
    public GameObject projectilePrefab;
    public float shootInterval = 2.5f;
    private float lastShootTime = -Mathf.Infinity;
    public AudioClip shootSound;
    public AudioClip hitSound;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.position = new Vector3(Random.Range(-21.9f, 21.9f), 7.5f, 0f);
        }
        else if (collision.gameObject.tag == "PlayerLaser")
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position, 1f);
            transform.position = new Vector3(Random.Range(-21.9f, 21.9f), 7.5f, 0f);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "ShooterEnemy")
        {
            transform.position = new Vector3(Random.Range(-21.9f, 21.9f), 7.5f, 0f);
        }
    }

    void FixedUpdate()
    {
        if (Time.time - lastShootTime >= shootInterval)
        {
            Vector3 shootDirection = (playerTransform.position - transform.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = shootDirection * 5.0f;
            AudioSource.PlayClipAtPoint(shootSound, transform.position, 1f);
            lastShootTime = Time.time;
        }
    }
}
