using UnityEngine;
using System.Collections;

public class ChainedEnemy : Enemy
{
    public ChainCollisions chainCollisions;
    public float jumpForce;
    public float rotationSpeed;

    private Rigidbody _rb;
    private Vector3 initialPosition;
    private float _jumpCooldown;
    private float _rotationCooldown;
    private float _rotationTimer;
    private float _onRangeTimer;
    private bool _rotationActivated;
    private bool _onRange;

    public override void Start()
    {
        base.Start();
        initialPosition = transform.position;
        _rb = gameObject.GetComponent<Rigidbody>();
        _onRange = true;
    }

    public override void FixedUpdate()
    {
        if (!Dead)
        {
            base.FixedUpdate();
            ControlRotation();
            Chase();
            CheckFarnessToTarget();
            CheckOnRange();
            GoBack();
            Die();
        }
    }

    public void ControlRotation()
    {
        if (_rotationActivated && _onRange)
        {
            transform.Rotate(new Vector3(0f, 1f, 0f), rotationSpeed);
            _rotationTimer += Time.deltaTime;
        }

        if (_rotationTimer >= Random.Range(3f, 5f))
        {
            _rotationActivated = false;
            _rotationTimer = 0f;
        }
    }

    public void Chase()
    {
        _jumpCooldown += Time.deltaTime;

        if (!_rotationActivated)
        {
            if (targetInSight && _onRangeTimer <= 0f)
            {
                _rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((targetReference.transform.position + new Vector3(0, 2f, 0)) - transform.position), rotationSpeed * Time.deltaTime);
                _rb.MovePosition(transform.position + transform.forward * (speed * 2) * Time.deltaTime);

                if (_jumpCooldown >= 1f)
                {
                    _rb.AddForce(Vector3.up * (jumpForce * _rb.mass));
                    _jumpCooldown = 0f;
                }
            }
        }
    }

    public void CheckFarnessToTarget()
    {
        if (!_rotationActivated) _rotationCooldown += Time.deltaTime;

        if(_rotationCooldown >= 5f)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 25f);

            foreach (var collider in hitColliders)
            {
                if (collider.gameObject.layer == K.LAYER_PLAYER)
                {
                    _rotationActivated = true;
                    _rotationCooldown = 0f;
                }
            }
        }
    }

    public void CheckOnRange()
    {
        _onRangeTimer -= Time.deltaTime;

        var distance = Vector3.Distance(initialPosition, transform.position);

        if(!_rotationActivated)
        {
            if (distance >= maxDistance) _onRange = false;
            else _onRange = true;
        }

        if (!_onRange) _onRangeTimer = 2f;
    }

    public void GoBack()
    {
        if (_onRangeTimer >= 0 || !targetInSight)
        {
            /*_rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((initialPosition) - transform.position), rotationSpeed * Time.deltaTime);
            _rb.MovePosition(transform.position + transform.forward * (speed * 2) * Time.deltaTime);*/

            if (_jumpCooldown >= 1f)
            {
                _rb.AddForce(Vector3.up * (jumpForce * _rb.mass));
                _jumpCooldown = 0f;
            }
        }
    }
}