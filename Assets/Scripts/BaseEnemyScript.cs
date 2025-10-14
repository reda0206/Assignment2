using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyScript : MonoBehaviour
{
    public float flashDuration = 0.5f;
    public Color flashColor = Color.red;
    private Transform playerTransform;
    public AudioClip hitSound;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }


    void FixedUpdate()
    {
        transform.position += new Vector3(0, -0.15f, 0);
        if (playerTransform != null)
        {
            float direction = Mathf.Sign(playerTransform.position.x - transform.position.x);
            if (Mathf.Abs(playerTransform.position.x - transform.position.x) > 0.05f)
            {
                transform.position += new Vector3(direction * 0.15f, 0, 0);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.position = new Vector3(Random.Range(-21.9f, 21.9f), 11.8f, 0f);
        }
        else if (collision.gameObject.tag == "PlayerLaser")
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position, 1f);
            transform.position = new Vector3(Random.Range(-21.9f, 21.9f), 11.8f, 0f);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "EnemyTeleporter")
        {
            transform.position = new Vector3(Random.Range(-21.9f, 21.9f), 11.8f, 0f);
        }
    }
}
