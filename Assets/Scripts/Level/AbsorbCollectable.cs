using UnityEngine;
using System.Collections;

public class AbsorbCollectable : MonoBehaviour
{
    public Player superMetalBall;
    public Player superGlassBall;
    public Player superGlassBallBreak01;
    public Player superGlassBallBreak02;

    private Player _player;
    private Vector3 _startPosition;
    private Vector3 _driftPosition;
    private Quaternion _startRotation;
    private Quaternion _driftRotation;
    private float _driftSeconds = 1.5f;
    private float _driftTimer;
    private bool _isDrifting;

    private void Update()
    {
        PlayerReference();
        PlayerAbsorb();
        CheckCollision();
        DestroyDefective();
    }

    private void PlayerReference()
    {
        if (superMetalBall.gameObject.activeSelf) _player = superMetalBall;
        if (superGlassBall.gameObject.activeSelf) _player = superGlassBall;
        if (superGlassBallBreak01.gameObject.activeSelf) _player = superGlassBallBreak01;
        if (superGlassBallBreak02.gameObject.activeSelf) _player = superGlassBallBreak02;

        if(_player != null)
        {
            _startPosition = _player.transform.position;
            _startRotation = transform.rotation;
        }
    }

    public void StartDrift()
    {
        _isDrifting = true;
        _driftTimer = 0;

        _driftPosition = transform.position;
        _driftRotation = transform.rotation;
    }

    private void PlayerAbsorb()
    {
        if (_isDrifting)
        {
            _driftTimer += Time.deltaTime;

            if (_driftTimer <= _driftSeconds)
            {
                var ratio = _driftTimer / _driftSeconds;

                transform.position = Vector3.Lerp(_driftPosition, _startPosition, ratio);
                transform.rotation = Quaternion.Slerp(_driftRotation, _startRotation, ratio);
            }
        }
    }

    private void CheckCollision()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.2f);

        foreach (var collider in hitColliders)
        {
            if (collider.gameObject.layer == K.LAYER_PLAYER)
            {
                Player hitPlayer;
                hitPlayer = collider.GetComponent<Player>();

                if (hitPlayer is Player) if (_isDrifting) Destroy(this.gameObject);
            }
        }
    }

    private void DestroyDefective()
    {
        if (_driftTimer >= _driftSeconds * 1.1f) Destroy(this.gameObject);
    }
}