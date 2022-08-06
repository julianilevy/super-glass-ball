using UnityEngine;
using System.Collections;

public class PendulumBall : MonoBehaviour
{
    public Waypoint[] waypoints;
    public Axis axis;
    public float speed;
    public float waitingTime;

    private Vector3 _direction;
    private float _waitingTime;
    private int _currentWaypoint = 1;
    private int _damage = 3;
    private bool _changeDirection;
    private bool _isWaitingTime;

    public enum Axis
    {
        Right,
        Left
    }

    private void Start()
    {
        _waitingTime = waitingTime;

        if (axis == Axis.Right) _direction = Vector3.right;
        if (axis == Axis.Left) _direction = Vector3.left;
    }

	private void FixedUpdate ()
    {
        WaypointsMovement();
	}

    private void WaypointsMovement()
    {
        if (waypoints.Length > 0)
        {
            if (!_isWaitingTime) transform.position += _direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, waypoints[_currentWaypoint].transform.position) <= 2)
            {
                _changeDirection = true;

                if (_currentWaypoint < waypoints.Length - 1) _currentWaypoint++;
                else _currentWaypoint = 0;
            }
        }

        if (_changeDirection)
        {
            _waitingTime -= Time.deltaTime;
            _isWaitingTime = true;

            if (_waitingTime <= 0)
            {
                _direction *= -1;
                _waitingTime = waitingTime;
                _changeDirection = false;
                _isWaitingTime = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == K.LAYER_ENEMY)
        {
            Enemy hitEnemy;
            hitEnemy = collision.gameObject.GetComponentInParent<Enemy>();

            if (hitEnemy is Enemy) hitEnemy.TakeDamage(_damage);
        }

        if (collision.gameObject.layer == K.LAYER_GLASS)
        {
            Breakable hitBreakable;
            hitBreakable = collision.gameObject.GetComponent<Breakable>();

            if (hitBreakable is Breakable) hitBreakable.BreakGlass();
        }
    }
}