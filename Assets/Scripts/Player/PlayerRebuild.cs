using UnityEngine;
using System.Collections;

public class PlayerRebuild : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _startPosition;
    private Vector3 _driftPosition;
    private Quaternion _startRotation;
    private Quaternion _driftRotation;
    private float _driftSeconds = 3;
    private float _driftTimer;
    private bool _isDrifting;

    private void Update()
    {
        Rebuild();
    }
	
    public void StartDrift ()
    {
        var playerStats = GameObject.FindObjectOfType<PlayerStats>();
        _rb = gameObject.GetComponent<Rigidbody>();

        _startPosition = playerStats.lastCheckpoint.transform.position + new Vector3(-0.27f, -0.92f, -0.06f);
        _startRotation = playerStats.lastCheckpoint.transform.rotation;

        _isDrifting = true;
        _driftTimer = 0;

        _driftPosition = transform.position;
        _driftRotation = transform.rotation;

        _rb.velocity = Vector3.zero;
    }

    private void StopDrift ()
    {
        _isDrifting = false;

        transform.position = _startPosition;
        transform.rotation = _startRotation;

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        _rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void Rebuild()
    {
        if (_isDrifting)
        {
            _driftTimer += Time.deltaTime;

            if (_driftTimer > _driftSeconds) StopDrift();
            else
            {
                var ratio = _driftTimer / _driftSeconds;

                transform.position = Vector3.Lerp(_driftPosition, _startPosition, ratio);
                transform.rotation = Quaternion.Slerp(_driftRotation, _startRotation, ratio);
            }
        }
    }
}
