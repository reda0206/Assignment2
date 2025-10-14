using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BossEnemyScript : MonoBehaviour
{
    private Transform playerTransform;
    public GameObject projectilePrefab;
    public float shootInterval = .5f;
    private float lastShootTime = -Mathf.Infinity;
    public float health = 30f;
    public TextMeshProUGUI bossHealthText;
    public Color flashColor = Color.clear;
    public float flashDuration = 0.1f;
    public AudioClip shootSound;
    public AudioClip hitSound;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        bossHealthText.text = "Boss Health: " + health.ToString();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerLaser")
        {
            health -= 1f;
            bossHealthText.text = "Boss Health: " + health.ToString();
            StartCoroutine(DamageFlash());
            AudioSource.PlayClipAtPoint(hitSound, transform.position, 1f);
            Destroy(collision.gameObject);
            if (health <= 0)
            {
                SceneManager.LoadScene("WinScene");
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(0.10f, 0, 0f);

        if (transform.position.x > 18.4f)
        {
            transform.position = new Vector3(-18.4f, transform.position.y, transform.position.z);
        }

        if (Time.time - lastShootTime >= shootInterval)
        {
            Vector3 shootDirection = (playerTransform.position - transform.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = shootDirection * 10.0f;
            AudioSource.PlayClipAtPoint(shootSound, transform.position, 1f);
            lastShootTime = Time.time;
        }
    }

    private IEnumerator DamageFlash()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Color originalColor = Color.white;
        float elapsedTime = 0f;
        while (elapsedTime < flashDuration)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
            elapsedTime += 0.2f;
        }
        spriteRenderer.color = originalColor;
    }
}
