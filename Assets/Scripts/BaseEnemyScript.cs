using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyScript : MonoBehaviour
{
    private Transform playerTransform;
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
        transform.position += new Vector3(0, -0.1f, 0);
        
        if (playerTransform != null)
        {
            float direction = Mathf.Sign(playerTransform.position.x - transform.position.x);
            if (Mathf.Abs(playerTransform.position.x - transform.position.x) > 0.05f)
            {
                transform.position += new Vector3(direction * 0.1f, 0, 0);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "EnemyTeleporter")
        {
            transform.position = new Vector3(Random.Range(-21.9f, 21.9f), 11.8f, 0f);
        }
    }
}
