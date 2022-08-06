using UnityEngine;
using System.Collections;

public class GermanFireball : MonoBehaviour
{
    public ParticleSystem child;
    public int damage;

    private float speedDivider;
    private bool _hitted;
    private bool _rbAdded;

    public void Start()
    {
        Destroy(this.gameObject, 10f);
    }

    public void Update()
    {
        if (!_rbAdded)
        {
            Move();
            CheckCollisions();
        }
    }

    public void Move()
    {
        transform.position += transform.forward / speedDivider;
    }

    public void SetSpeedDivider(float speed)
    {
        speedDivider = speed;
    }

    public void Deactivate()
    {
        if (!_rbAdded)
        {
            gameObject.AddComponent<Rigidbody>();
            gameObject.GetComponent<Rigidbody>().mass = 0.01f;
            gameObject.GetComponent<ParticleSystem>().enableEmission = false;
            child.enableEmission = false;

            _rbAdded = true;
        }
    }

    public void SetTarget(Transform targetRef)
    {
        var dirToTarget = targetRef.position - transform.position;
        dirToTarget.y = transform.forward.y;

        transform.forward = dirToTarget;
    }

    public void CheckCollisions()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.6f);

        foreach (var collider in hitColliders)
        {
            if (!_hitted)
            {
                if (collider.gameObject.layer == K.LAYER_PLAYER)
                {
                    Player hitPlayer;
                    hitPlayer = collider.GetComponent<Player>();
                    if (hitPlayer is Player) if (!hitPlayer.playerStats.powered && !_hitted) hitPlayer.playerStats.TakeDamage(damage);

                    _hitted = true;
                }
            }

            if (collider.gameObject.layer == K.LAYER_ENEMY)
            {
                GermanSpikedEnemy hitGermanSpikedEnemy;
                hitGermanSpikedEnemy = collider.GetComponentInParent<GermanSpikedEnemy>();

                if (hitGermanSpikedEnemy is GermanSpikedEnemy) if (!_hitted) hitGermanSpikedEnemy.TakeDamage(3);
            }

            if (collider.gameObject.layer == K.LAYER_GLASS)
            {
                Breakable hitBreakable;
                hitBreakable = collider.GetComponent<Breakable>();
                if (hitBreakable is Breakable) hitBreakable.BreakGlass();
            }
        }
    }
}
