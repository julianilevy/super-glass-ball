using UnityEngine;
using System.Collections;

public class Enemy : Entity
{
    public Waypoint[] waypoints;
    public Player superGlassBall;
    public Player superGlassBallBreak01;
    public Player superGlassBallBreak02;
    public GameObject destroyedEnemy;
    public float viewAngle;
    public float viewDistance;
    public float maxDistance;
    public bool targetInSight;
    public bool deadRespawn;

    protected Player targetReference;
    protected int currentWaypoint;

    private Vector3 _dirToTarget;
    private float _angleToTarget;
    private float _distanceToTarget;
    private float _distanceFromStart;
    private float _backVelocityTimer;
    private float _explosionHitCD;
    private bool _velocityToBack;
    private bool _maxDistanceReached;
    private bool _hittedByExplosion;
    private bool _dead;

    public bool MaxDistanceReached
    {
        get { return _maxDistanceReached; }
        set { _maxDistanceReached = value; }
    }

    public bool HittedByExplosion
    {
        get { return _hittedByExplosion; }
        set { _hittedByExplosion = value; }
    }

    public bool Dead
    {
        get { return _dead; }
        set { _dead = value; }
    }
	
	public override void Update()
    {
        base.Update();
	}

    public virtual void FixedUpdate()
    {
        UpdateTargetReference();
        WaypointsMovement();
        Detection();
        CheckFarness();
        ExplosionHit();
    }

    protected void UpdateTargetReference()
    {
        if (superGlassBall.gameObject.activeSelf) targetReference = superGlassBall;
        if (superGlassBallBreak01.gameObject.activeSelf) targetReference = superGlassBallBreak01;
        if (superGlassBallBreak02.gameObject.activeSelf) targetReference = superGlassBallBreak02;
    }

    protected void WaypointsMovement()
    {
        if(waypoints.Length > 0)
        {
            if (!targetInSight || _maxDistanceReached)
            {
                var dirToWaypoint = waypoints[currentWaypoint].transform.position - transform.position;

                transform.forward = Vector3.Slerp(transform.forward, dirToWaypoint, Time.deltaTime);

                if (_maxDistanceReached || _velocityToBack) transform.position += transform.forward * (speed * 4) * Time.deltaTime;
                else transform.position += transform.forward * speed * Time.deltaTime;

                if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) <= 1.5f)
                {
                    if (currentWaypoint < waypoints.Length - 1) currentWaypoint++;
                    else currentWaypoint = 0;
                }
            }
        }
    }

    protected void Detection()
    {
        if(!_maxDistanceReached)
        {
            if(targetReference != null)
            {
                if(targetReference.gameObject.activeSelf)
                {
                    _dirToTarget = targetReference.transform.position - transform.position;

                    _angleToTarget = Vector3.Angle(transform.forward, _dirToTarget);

                    _distanceToTarget = Vector3.Distance(transform.position, targetReference.transform.position);

                    if (_angleToTarget <= viewAngle && _distanceToTarget <= viewDistance) targetInSight = true;
                    else targetInSight = false;

                    if (targetInSight) _backVelocityTimer = 1f;
                    else _backVelocityTimer -= Time.deltaTime;
                    if (!targetInSight && _backVelocityTimer >= 0f) _velocityToBack = true;
                    else _velocityToBack = false;
                }
            }
        }
    }

    protected void CheckFarness()
    {
        if (waypoints.Length > 0) _distanceFromStart = Vector3.Distance(transform.position, waypoints[0].gameObject.transform.position);

        if (_distanceFromStart >= maxDistance) _maxDistanceReached = true;
        if (_maxDistanceReached && _distanceFromStart <= maxDistance - 10f) _maxDistanceReached = false;
    }

    protected void ExplosionHit()
    {
        if (HittedByExplosion) _explosionHitCD += Time.deltaTime;

        if (HittedByExplosion && _explosionHitCD >= 2.5f)
        {
            HittedByExplosion = false;
            _explosionHitCD = 0f;
        }
    }

    protected virtual void Die()
    {
        if (currentHealth <= 0)
        {
            var corpse = Instantiate(destroyedEnemy, transform.position, transform.rotation);
            Destroy(corpse, 20f);
            Dead = true;
            Destroy(this.gameObject, 0.1f);
        }
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == K.LAYER_MAPLIMIT)
        {
            currentHealth -= 1000;
        }
    }

    public void OnDrawGizmos()
    {
        /*Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward * viewDistance));

        Vector3 rightLimit = Quaternion.AngleAxis(viewAngle, transform.up) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + (rightLimit * viewDistance));

        Vector3 leftLimit = Quaternion.AngleAxis(-viewAngle, transform.up) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + (leftLimit * viewDistance));*/
    }
}
