using UnityEngine;
using System.Collections;

public class SpikedEnemy : Enemy
{
    public GameObject spikedEnemy;
    public float rotationSpeed;

    public override void FixedUpdate()
    {
        UpdateTargetReference();
        Detection();
        CheckFarness();
        ExplosionHit();

        if(!Dead)
        {
            WaypointsMovement();
            ControlRotation();
            Chase();
            Die();
        }
    }

    public void ControlRotation()
    {
        if (!targetInSight) spikedEnemy.transform.Rotate(new Vector3(0f, 1f, 0f), rotationSpeed);
        else spikedEnemy.transform.Rotate(new Vector3(0f, 1f, 0f), rotationSpeed * 3f);
    }

    public void Chase()
    {
        if (targetReference != null)
        {
            if (targetInSight && !MaxDistanceReached)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetReference.transform.position - transform.position), rotationSpeed * Time.deltaTime);
                transform.position += transform.forward * (speed * 2) * Time.deltaTime;
            }
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == K.LAYER_ENEMY)
        {
            Enemy hitEnemy;
            hitEnemy = collision.gameObject.GetComponentInParent<Enemy>();

            if (hitEnemy is Enemy) hitEnemy.TakeDamage(damage);
        }

        if (collision.gameObject.layer == K.LAYER_GLASS)
        {
            Breakable hitBreakable;
            hitBreakable = collision.gameObject.GetComponent<Breakable>();

            if (hitBreakable is Breakable) hitBreakable.BreakGlass();
        }
    }
}