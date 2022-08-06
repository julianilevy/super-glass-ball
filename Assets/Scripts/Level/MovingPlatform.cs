using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public Waypoint[] waypoints;
    public Rigidbody rb;
    public Axis axis;
    public float speed;

    private Vector3 _direction;
    private float _rotationTimer;
    private int _currentWaypoint = 1;
    private bool _rotationActivated;

    public enum Axis
    {
        VerticalUp,
        VerticalDown,
        HorizontalForward,
        HorizontalBack,
        HorizontalRight,
        HorizontalLeft
    }

    private void Start()
    {
        if (axis == Axis.VerticalUp) _direction = Vector3.up;
        if (axis == Axis.VerticalDown) _direction = Vector3.down;
        if (axis == Axis.HorizontalForward) _direction = Vector3.forward;
        if (axis == Axis.HorizontalBack) _direction = Vector3.back;
        if (axis == Axis.HorizontalRight) _direction = Vector3.right;
        if (axis == Axis.HorizontalLeft) _direction = Vector3.left;
    }

	private void FixedUpdate ()
    {
        WaypointsMovement();
        ControlRotation();
	}

    private void WaypointsMovement()
    {
        if (waypoints.Length > 0)
        {
            if(!_rotationActivated)
            {
                rb.MovePosition(transform.position + _direction * speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, waypoints[_currentWaypoint].transform.position) <= 2)
                {
                    _rotationActivated = true;

                    if (_currentWaypoint < waypoints.Length - 1) _currentWaypoint++;
                    else _currentWaypoint = 0;
                }
            }
        }
    }

    private void ControlRotation()
    {
        if (_rotationActivated)
        {
            _rotationTimer += Time.deltaTime;

            if (axis == Axis.VerticalUp || axis == Axis.HorizontalForward) transform.Rotate(new Vector3(1, 0, 0), 110f * Time.deltaTime);
            if (axis == Axis.VerticalDown || axis == Axis.HorizontalBack) transform.Rotate(new Vector3(-1, 0, 0), 110f * Time.deltaTime);
            if (axis == Axis.HorizontalRight) transform.Rotate(new Vector3(0, 0, -1), 110f * Time.deltaTime);
            if (axis == Axis.HorizontalLeft) transform.Rotate(new Vector3(0, 0, 1), 110f * Time.deltaTime);
        }

        if(_rotationTimer >= 1.63f)
        {
            if (axis == Axis.VerticalUp) EndRotationX(Vector3.down, Vector3.up);
            if (axis == Axis.VerticalDown) EndRotationX(Vector3.up, Vector3.down);
            if (axis == Axis.HorizontalForward) EndRotationX(Vector3.back, Vector3.forward);
            if (axis == Axis.HorizontalBack) EndRotationX(Vector3.forward, Vector3.back);
            if (axis == Axis.HorizontalRight) EndRotationZ(Vector3.left, Vector3.right);
            if (axis == Axis.HorizontalLeft) EndRotationZ(Vector3.right, Vector3.left);

            _rotationActivated = false;
            _rotationTimer = 0f;
        }
    }

    private void EndRotationX(Vector3 vector1, Vector3 vector2)
    {
        if(_currentWaypoint == 0)
        {
            _direction = vector1;
            transform.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
        }
        else
        {
            _direction = vector2;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }

    private void EndRotationZ(Vector3 vector1, Vector3 vector2)
    {
        if (_currentWaypoint == 0)
        {
            _direction = vector1;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        else
        {
            _direction = vector2;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}