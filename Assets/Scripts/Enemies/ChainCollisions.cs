using UnityEngine;
using System.Collections;

public class ChainCollisions : MonoBehaviour
{
    public ChainedEnemy chainedEnemy;

    private bool _hingeJointDestroyed;

    public void OnCollisionEnter(Collision collision)
    {
        if(!chainedEnemy.Dead)
        {
            if (collision.gameObject.layer == K.LAYER_PLAYER)
            {
                Player hitPlayer;
                hitPlayer = collision.gameObject.GetComponent<Player>();

                if (hitPlayer is Player)
                {
                    if (!hitPlayer.playerStats.powered) hitPlayer.playerStats.TakeDamage(chainedEnemy.damage);
                    if (hitPlayer.playerStats.powered && !chainedEnemy.Dead) chainedEnemy.TakeDamage(hitPlayer.playerStats.damage);
                }
            }

            if (collision.gameObject.layer == K.LAYER_ENEMY)
            {
                Enemy hitEnemy;
                hitEnemy = collision.gameObject.GetComponentInParent<Enemy>();

                if (hitEnemy is Enemy) hitEnemy.TakeDamage(chainedEnemy.damage);
            }

            if (collision.gameObject.layer == K.LAYER_GLASS)
            {
                Breakable hitBreakable;
                hitBreakable = collision.gameObject.GetComponent<Breakable>();

                if (hitBreakable is Breakable) hitBreakable.BreakGlass();
            }
        }
    }

    public void DestroyChildHinge()
    {
        foreach (Transform child in transform)
        {
            if (transform.childCount > 0 && !_hingeJointDestroyed)
            {
                Component.Destroy(child.gameObject.GetComponent<HingeJoint>());
                child.GetComponent<ChainCollisions>().DestroyChildHinge();
                _hingeJointDestroyed = true;
            }
        }
    }
}
