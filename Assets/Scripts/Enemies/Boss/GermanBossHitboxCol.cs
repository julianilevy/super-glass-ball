using UnityEngine;
using System.Collections;

public class GermanBossHitboxCol : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == K.LAYER_ENEMY)
        {
            GermanSpikedEnemy hitGermanSpikedEnemy;
            hitGermanSpikedEnemy = collision.gameObject.GetComponentInParent<GermanSpikedEnemy>();

            if (hitGermanSpikedEnemy is GermanSpikedEnemy) hitGermanSpikedEnemy.TakeDamage(3);
        }
    }
}
