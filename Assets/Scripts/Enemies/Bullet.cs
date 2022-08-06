using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public int damage;

    private bool _hitted;
    private bool _rbAdded;

	public void Start()
    {
        Destroy(this.gameObject, 20f);
	}

    public void Update()
    {
        Move();
        CheckCollisions();
    }

    public void Move()
    {
        if (!_hitted) transform.position += transform.forward / 1.5f;
        else
        {
            if(!_rbAdded)
            {
                gameObject.AddComponent<Rigidbody>();
                gameObject.GetComponent<Rigidbody>().mass = 0.01f;
                Destroy(gameObject.GetComponent<TrailRenderer>());

                _rbAdded = true;
            }
        }
    }

    public void CheckCollisions()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.5f);

        foreach (var collider in hitColliders)
        {
            if (collider.gameObject.layer == K.LAYER_FLOOR) _hitted = true;
            if (collider.gameObject.layer == K.LAYER_HARMFULOBJECT) _hitted = true;

            if (collider.gameObject.layer == K.LAYER_PLAYER)
            {
                Player hitPlayer;
                hitPlayer = collider.GetComponent<Player>();
                if (hitPlayer is Player) if (!hitPlayer.playerStats.powered && !_hitted) hitPlayer.playerStats.TakeDamage(damage);

                _hitted = true;
            }

            if (collider.gameObject.layer == K.LAYER_ENEMY)
            {
                Enemy hitEnemy;
                hitEnemy = collider.GetComponentInParent<Enemy>();
                if (hitEnemy is Enemy) if (!_hitted) hitEnemy.TakeDamage(damage);

                _hitted = true;
            }

            if (collider.gameObject.layer == K.LAYER_GLASS)
            {
                Breakable hitBreakable;
                hitBreakable = collider.GetComponent<Breakable>();
                if (hitBreakable is Breakable) hitBreakable.BreakGlass();

                _hitted = true;
            }
        }
    }
}
