using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float lives = 3;
    public float respawnTime = 2.0f;
    private float lastRespawnTime = -Mathf.Infinity;
    public bool isInvincible = false;
    public float flashDuration = 2.5f;
    public Color flashColor = Color.clear;
    public GameObject playerProjectile;
    public float shootCooldown = 0.5f;
    private float lastShootTime = -Mathf.Infinity;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(0.3f, 0, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-0.3f, 0, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 0.3f, 0);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, -0.3f, 0);
        }
        if (Input.GetKey(KeyCode.Space) && Time.time - lastShootTime >= shootCooldown)
        {
            Instantiate(playerProjectile, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
            playerProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10.0f);
            lastShootTime = Time.time;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !isInvincible)
        {
            LoseLife();
        }

        else if (collision.gameObject.tag == "Laser" && !isInvincible)
        {
            LoseLife();
            Destroy(collision.gameObject);
        }
    }
    public void LoseLife()
    {
               lives -= 1;
        if (lives > 0)
        {
         Respawn();
        }
        else
        {
            Debug.Log("Game Over");
            Destroy(gameObject);
        }
    }

    public void Respawn()
    {
        if (Time.time - lastRespawnTime >= respawnTime)
        {
            transform.position = new Vector3(0, -7.5f, 0);
            lastRespawnTime = Time.time;
            StartCoroutine(InvincibilityCoroutine());
        }
    }
    
    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        if (flashDuration > 0)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            Color originalColor = spriteRenderer.color;
            float elapsedTime = 0f;
            while (elapsedTime < flashDuration)
            {
                spriteRenderer.color = flashColor;
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.color = originalColor;
                yield return new WaitForSeconds(0.1f);
                elapsedTime += 0.2f;
            }
            spriteRenderer.color = originalColor;
        }
        isInvincible = false;
    }
}
