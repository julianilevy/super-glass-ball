using UnityEngine;
using System.Collections;

public class PowerSlam : Power, IPower
{
    public Explosion explosion;
    public GameObject tray;
    public GameObject steam;

    private Player _currentBall;
    private GameObject _tray;
    private GameObject _steam;
    private bool _jumpDone;
    private bool _ableToExplode;
    private bool _fallDone;
    private bool _explosionDone;
    private bool _steamDone;
    private bool _lessAmmoDone;
    private float _ableToExplodeTimer = 0.5f;
    private float _timeToFall = 2f;
    private float _timeToGlass = 1.5f;
    private float _timeToFixError = 5f;

    public void FixedUpdate()
    {
        if(playerStats.powerSlamON)
        {
            CheckCollisions();
            DoJump();
            DoChangeToMetal();
            DoFall();
            UpdateTray();
            DoChangeToGlass();
            IfDestroyed();
        }
    }

    public void TriggerPower()
    {
        ActivatePowerSlam();
    }

    public void CheckCollisions()
    {
        if(_jumpDone && !toMetalDone)
        {
            Collider[] hitColliders = Physics.OverlapSphere(_currentBall.transform.position, _currentBall.radius);

            foreach (var collider in hitColliders)
            {
                if (collider.gameObject.layer == K.LAYER_FLOOR) _timeToFall = 0.35f;
                if (collider.gameObject.layer == K.LAYER_HARMFULOBJECT) _timeToFall = 0.35f;
                if (collider.gameObject.layer == K.LAYER_ENEMY) _timeToFall = 0.35f;
            }
        }
    }

    public void DoJump()
    {
        if (!_jumpDone)
        {
            foreach (var ballMesh in allBallsMeshes) if (ballMesh.gameObject.activeSelf) _currentBall = ballMesh;

            playerStats.speed = 0f;
            _currentBall.rb.AddForce(new Vector3(0, 1, 0) * (playerStats.jumpForce * 400));
            _jumpDone = true;
        }
    }

    public void DoChangeToMetal()
    {
        if(_timeToFall <= 0.35f && !toMetalDone)
        {
            ChangeMeshToMetal();
            toMetalDone = true;
            _ableToExplode = true;
        }
    }

    public void DoFall()
    {
        if (_jumpDone && !_fallDone) _timeToFall -= Time.deltaTime;
        if (_ableToExplode) _ableToExplodeTimer -= Time.deltaTime;

        if(_timeToFall <= 0 && !_fallDone)
        {
            _tray = (GameObject)Instantiate(tray, superMetalBall.transform.position + new Vector3(0, -3f, 0), tray.transform.rotation) as GameObject;
            Destroy(_tray, 5);
            superMetalBall.rb.AddForce(new Vector3(0, 1, 0) * (playerStats.jumpForce * -1000));
            _fallDone = true;
        }
    }

    public void UpdateTray()
    {
        if (_tray != null) _tray.transform.position = superMetalBall.transform.position + new Vector3(0, -3f, 0);
    }

    public void DisableTray()
    {
        if (_tray != null)
        {
            _tray.GetComponent<ParticleSystem>().enableEmission = false;

            for (int i = 0; i < _tray.transform.childCount; ++i)
            {
                _tray.transform.GetChild(i).GetComponent<ParticleSystem>().enableEmission = false;
            }
        }
    }

    public void DoChangeToGlass()
    {
        if (_ableToExplodeTimer <= 0)
        {
            if (_fallDone) _timeToFixError -= Time.deltaTime;

            if (_fallDone && superMetalBall.Grounded)
            {
                _timeToGlass -= Time.deltaTime;
                DisableTray();
                Destroy(_tray.gameObject, 7f);

                if (!_lessAmmoDone)
                {
                    playerStats.ammo--;
                    _lessAmmoDone = true;
                }
            }

            DoSteam();
            DoExplosion();

            if (!toGlassDone)
            {
                if (_timeToGlass <= 0 || _timeToFixError <= 0)
                {
                    toGlassDone = true;
                    ChangeMeshToGlass();
                    ResetAll();
                }
            }
        }
    }

    public void IfDestroyed()
    {
        if (!superMetalBall.gameObject.activeSelf && toMetalDone)
        {
            if (!_lessAmmoDone)
            {
                playerStats.ammo--;
                _lessAmmoDone = true;
            }

            ResetAll();
        }
    }

    public void DoSteam()
    {
        if (superMetalBall.Grounded && _fallDone && !_steamDone)
        {
            _steam = (GameObject)Instantiate(steam, superMetalBall.transform.position + new Vector3(0, -1.1f, 0), steam.transform.rotation) as GameObject;
            Destroy(_steam, 7f);
            _steamDone = true;
        }
    }

    public void DoExplosion()
    {
        if (superMetalBall.Grounded && !superMetalBall.LastHitBreakable && _fallDone && !_explosionDone)
        {
            Instantiate(explosion, superMetalBall.transform.position + new Vector3(0, 0.1f, 0), explosion.transform.rotation);
            _explosionDone = true;
        }
    }

    public void ResetAll()
    {
        superMetalBall.Grounded = false;
        superMetalBall.rb.velocity = Vector3.zero;
        superMetalBall.rb.angularVelocity = Vector3.zero;
        toMetalDone = false;
        toGlassDone = false;
        _jumpDone = false;
        _ableToExplode = false;
        _fallDone = false;
        _steamDone = false;
        _explosionDone = false;
        _lessAmmoDone = false;
        _ableToExplodeTimer = 0.5f;
        _timeToFall = 2f;
        _timeToGlass = 1.5f;
        _timeToFixError = 5f;
        playerStats.speed = 10;
        DeactivatePowerSlam();
        if (_steam != null) _steam.GetComponent<ParticleSystem>().enableEmission = false;
    }
}