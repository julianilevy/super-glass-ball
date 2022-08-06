using UnityEngine;
using System.Collections;

public class PowerSpeed : Power, IPower
{
    public GameObject sparks;
    public GameObject steam;
    public GameObject smoke;

    private GameObject _spark01;
    private GameObject _spark02;
    private GameObject _spark03;
    private GameObject _steam;
    private GameObject _smoke;
    private bool _sparksActivated;
    private bool _steamActivated;
    private bool _chargeDone;
    private bool _fireActivated;
    private bool _runDone;
    private bool _stopDone;
    private bool _smokeActivated;
    private float _chargeTimer = 3f;

    public void FixedUpdate()
    {
        if (playerStats.powerSpeedON)
        {
            DoChangeToMetal();
            DoCharge();
            DoRun();
            DoStop();
            DoChangeToGlass();

            if (!superMetalBall.gameObject.activeSelf && toMetalDone) ResetAll();
        }
    }

    public void TriggerPower()
    {
        ActivatePowerSpeed();
    }

    public void DoChangeToMetal()
    {
        if(!toMetalDone)
        {
            playerStats.speed = 0;
            ChangeMeshToMetal();
            toMetalDone = true;
        }
    }

    public void DoCharge()
    {
        if (toMetalDone)
        {
            _chargeTimer -= Time.deltaTime;
            playerStats.jumpForce = 0;
            CreateSparks();
            CreateSteam();
        }

        if(!_chargeDone && _chargeTimer >= 0)
        {
            superMetalBall.transform.Rotate(new Vector3(1, 0, 0), 90f, Space.World);
            _steam.transform.position = superMetalBall.transform.position + new Vector3(0, -0.94f, -0.94f);
            UpdateSparks();
        }

        if (!_chargeDone && _chargeTimer < 0)
        {
            playerStats.jumpForce = 8;
            _chargeDone = true;
            _fireActivated = true;
            Physics.gravity = new Vector3(0, -60, 0);
        }
    }

    public void CreateSparks()
    {
        if(!_sparksActivated)
        {
            _spark01 = (GameObject)Instantiate(sparks, superMetalBall.transform.position + new Vector3(-0.3f, -0.72f, -0.7f), sparks.transform.rotation) as GameObject;
            _spark02 = (GameObject)Instantiate(sparks, superMetalBall.transform.position + new Vector3(0, -0.72f, -0.80f), sparks.transform.rotation) as GameObject;
            _spark03 = (GameObject)Instantiate(sparks, superMetalBall.transform.position + new Vector3(0.3f, -0.72f, -0.7f), sparks.transform.rotation) as GameObject;

            Destroy(_spark01, _chargeTimer + 0.3f);
            Destroy(_spark02, _chargeTimer + 0.3f);
            Destroy(_spark03, _chargeTimer + 0.3f);

            _sparksActivated = true;
        }
    }

    public void UpdateSparks()
    {
        if (_spark01 != null)
        {
            _spark01.transform.position = superMetalBall.transform.position + new Vector3(-0.3f, -0.72f, -0.7f);
        }
        if (_spark02 != null)
        {
            _spark02.transform.position = superMetalBall.transform.position + new Vector3(0, -0.72f, -0.80f);
        }
        if (_spark03 != null)
        {
            _spark03.transform.position = superMetalBall.transform.position + new Vector3(0.3f, -0.72f, -0.7f);
        }
    }

    public void CreateSteam()
    {
        if (!_steamActivated)
        {
            _steam = (GameObject)Instantiate(steam, superMetalBall.transform.position + new Vector3(0, -0.94f, -0.94f), steam.transform.rotation) as GameObject;

            Destroy(_steam, _chargeTimer + 1f);

            _steamActivated = true;
        }
    }

    public void DoRun()
    {
        if (_chargeDone && !_runDone)
        {
            playerStats.speed = 50;
            playerStats.jumpForce = 17;

            if (superMetalBall.rb.velocity.z >= 100f) playerStats.speed = 1;

            if (superMetalBall.rb.velocity.z >= 50f) playerStats.running = true;
            else playerStats.running = false;

            superMetalBall.rb.AddForce(new Vector3(0, 0, 1) * playerStats.speed);

            if (!playerStats.fullingFuel) playerStats.fuel -= Time.deltaTime;
            if (playerStats.fuel < 0) playerStats.fuel = 0;
        }

        if(_fireActivated)
        {
            superMetalBall.GetComponent<ParticleSystem>().enableEmission = true;
            _fireActivated = false;
        }
    }

    public void DoStop()
    {
        if (playerStats.fuel <= 0f && !_runDone)
        {
            _runDone = true;
            playerStats.running = false;

            CreateSmoke();

            superMetalBall.rb.AddForce(new Vector3(0, 1, 0) * (playerStats.jumpForce * -100));
            superMetalBall.rb.velocity = Vector3.zero;
            superMetalBall.rb.angularVelocity = Vector3.zero;

            Physics.gravity = new Vector3(0, -30, 0);

            playerStats.speed = 0;
            playerStats.jumpForce = 0;
        }

        if (_runDone && !_stopDone)
        {
            superMetalBall.rb.angularDrag += Time.deltaTime * 1.5f;

            _smoke.transform.position = superMetalBall.transform.position;

            if (superMetalBall.rb.angularDrag >= 4f)
            {
                _stopDone = true;
                _smoke.GetComponent<ParticleSystem>().enableEmission = false;
            }

        }
    }

    public void CreateSmoke()
    {
        if (!_smokeActivated)
        {
            _smoke = (GameObject)Instantiate(smoke, superMetalBall.transform.position, smoke.transform.rotation) as GameObject;

            Destroy(_smoke, 7f);

            _smokeActivated = true;
        }
    }

    public void DoChangeToGlass()
    {
        if(_stopDone && !toGlassDone)
        {
            toGlassDone = true;
            superMetalBall.rb.velocity = superGlassBall.rb.velocity;
            superMetalBall.rb.angularVelocity = superGlassBall.rb.angularVelocity;
            superMetalBall.rb.angularDrag = superGlassBall.rb.angularDrag;
            ChangeMeshToGlass();
            ResetAll();
        }
    }

    public void ResetAll()
    {
        Physics.gravity = new Vector3(0, -30, 0);
        superMetalBall.Grounded = false;
        superMetalBall.rb.velocity = Vector3.zero;
        superMetalBall.rb.angularVelocity = Vector3.zero;
        toMetalDone = false;
        toGlassDone = false;
        _sparksActivated = false;
        _steamActivated = false;
        _chargeDone = false;
        _fireActivated = false;
        _runDone = false;
        _stopDone = false;
        _smokeActivated = false;
        _chargeTimer = 3f;
        playerStats.running = false;
        playerStats.powerSpeedON = false;
        playerStats.speed = 10;
        playerStats.jumpForce = 8;
        DeactivatePowerSpeed();
    }
}