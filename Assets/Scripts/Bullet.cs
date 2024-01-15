using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int minDamage;
    public int maxDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int damage = Random.Range(minDamage, maxDamage);
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
