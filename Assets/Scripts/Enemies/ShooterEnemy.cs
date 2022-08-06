using UnityEngine;
using System.Collections;

public class ShooterEnemy : Enemy
{
    public GameObject head;
    public Bullet bullet;
    public float headRotationSpeed;

    private float _shootTimer;

    public override void FixedUpdate()
    {
        UpdateTargetReference();
        Detection();
        CheckFarness();
        ExplosionHit();

        if (!Dead)
        {
            WaypointsMovement();
            ControlRotation();
            Shoot();
            Die();
        }
    }

    public void ControlRotation()
    {
        if (!targetInSight) head.transform.Rotate(new Vector3(0f, 0f, 1f), headRotationSpeed);
        else head.transform.Rotate(new Vector3(0f, 0f, 1f), headRotationSpeed * 5f);
    }

    public void Shoot()
    {
        if (targetInSight && !Dead)
        {
            _shootTimer += Time.deltaTime;

            transform.forward = targetReference.transform.position - transform.position;

            if(_shootTimer >= 0.25f)
            {
                Instantiate(bullet.gameObject, head.transform.position + head.transform.forward, transform.rotation);
                _shootTimer = 0;
            }
        }
    }
}