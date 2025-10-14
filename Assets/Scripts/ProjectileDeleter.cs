using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileDeleter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "PlayerLaser")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "BossLaser")
        {
            Destroy(collision.gameObject);
        }
    }
}
