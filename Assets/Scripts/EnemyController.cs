using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Player player;
    public int minDamage;
    public int maxDamage;

    PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
            InvokeRepeating("DamagePlayer", 0, 0.1f);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        player = collision.gameObject.GetComponent<Player>();
    //        InvokeRepeating("DamagePlayer", 0, 0.1f);
    //    }
    //    else
    //    {
    //        Debug.Log("Not player");
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        player = null;
    //        CancelInvoke("DamagePlayer");
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
            CancelInvoke("DamagePlayer");
        }
    }

    void DamagePlayer()
    {
        int damage = Random.Range(minDamage, maxDamage);
        player.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        playerHealth.EnemyTakeDame(damage);
    }
}
