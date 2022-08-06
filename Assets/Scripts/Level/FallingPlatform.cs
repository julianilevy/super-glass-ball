using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private float _timeToFall = 1f;
    private bool _fallActivated;

    private void Start ()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

	private void FixedUpdate ()
    {
        Fall();
        Respawn();
	}

    private void Fall()
    {
        if (_fallActivated) _timeToFall -= Time.deltaTime;

        if (_timeToFall <= 0)
        {
            _rb.isKinematic = false;
            _rb.useGravity = true;
        }
    }

    private void Respawn()
    {
        if(_timeToFall <= -4)
        {
            _rb.isKinematic = true;
            _rb.useGravity = false;
            transform.position = _initialPosition;
            transform.rotation = _initialRotation;
            _fallActivated = false;
            _timeToFall = 1f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == K.LAYER_PLAYER) _fallActivated = true;

        if (collision.gameObject.layer == K.LAYER_GLASS)
        {
            Breakable hitBreakable;
            hitBreakable = collision.gameObject.GetComponent<Breakable>();

            if (hitBreakable is Breakable) hitBreakable.BreakGlass();
        }
    }
}