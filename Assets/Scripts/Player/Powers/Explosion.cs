using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    private int _damage = 1000;
    private float _radius = 5f;
    private float _radiusMultiplier = 5f;
    private float _checkTime;
    private float _destroyTime = 2f;

    private void Update ()
    {
        Expand();
        ControlRadius();
        Disappear();
	}

    private void Expand()
    {
        _checkTime -= Time.deltaTime;
        _radius += Time.deltaTime * _radiusMultiplier;

        if (_checkTime <= 0)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius);

            foreach(var collider in hitColliders)
            {
                if (collider.gameObject.layer == K.LAYER_GLASS)
                {
                    Breakable hitBreakable;
                    hitBreakable = collider.GetComponent<Breakable>();

                    if (hitBreakable is Breakable)
                    {
                        hitBreakable.BreakGlass();
                    }
                }

                if (collider.gameObject.layer == K.LAYER_ENEMY)
                {
                    Enemy hitEnemy;
                    hitEnemy = collider.GetComponentInParent<Enemy>();

                    if (hitEnemy is Enemy)
                    {
                        if (!hitEnemy.HittedByExplosion)
                        {
                            hitEnemy.HittedByExplosion = true;
                            hitEnemy.TakeDamage(_damage);
                        }
                    }
                }

                if (collider.gameObject.layer == K.LAYER_BULLET)
                {
                    GermanFireball hitGermanFireball;
                    hitGermanFireball = collider.GetComponent<GermanFireball>();

                    if (hitGermanFireball is GermanFireball)
                    {
                        hitGermanFireball.Deactivate();
                    }
                }
            }

            _checkTime = 0.1f;
        }
    }

    private void ControlRadius()
    {
        if (_radius >= 6f) _radiusMultiplier = 40f;
    }

    private void Disappear()
    {
        _destroyTime -= Time.deltaTime;

        if (_destroyTime <= 0) Destroy(this.gameObject);
    }
}