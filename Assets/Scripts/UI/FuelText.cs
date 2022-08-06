using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FuelText : MonoBehaviour
{
    public PlayerStats playerStats;
    public ParticleSystem fire;
    public Image progress;

    private MoveTexts _parent;
    private float _updateFuelTimer;
    private float _currentFuel;
    private float _maxFuel = 5;

	private void Start ()
    {
        fire.enableEmission = false;
        _parent = gameObject.GetComponentInParent<MoveTexts>();
        _currentFuel = playerStats.fuel;
	}
	
	private void Update ()
    {
        ShowFuel();
        ActiveFire();
	}

    private void ShowFuel()
    {
        if (_currentFuel != playerStats.fuel) _parent.active = true;

        if (_parent.active)
        {
            _updateFuelTimer += Time.deltaTime;

            if (_currentFuel != playerStats.fuel)
            {
                if (_parent.StayTime >= 3) _parent.ReadyToBack = true;
                else _parent.StayTime = 0;
            }

            if (_parent.ReadyToBack)
            {
                _parent.TimeToDisappear -= Time.deltaTime;

                if (_parent.TimeToDisappear <= 0)
                {
                    _parent.ReadyToBack = false;
                    _parent.StayTime = 0;
                }
            }

            if (_updateFuelTimer >= 1.4f) _currentFuel = playerStats.fuel;
        }
        else _updateFuelTimer = 0;

        ManageFuel();
    }

    private void ActiveFire()
    {
        if (playerStats.powerSpeedON && playerStats.fuel > 0)
        {
            if (_parent.TimeToDisappear > 0)
            {
                _parent.TimeToDisappear -= Time.deltaTime;
                _parent.ReadyToBack = true;
            }
            else
            {
                _parent.active = true;
                _parent.StayTime = 0;
                fire.enableEmission = true;
            }
        }
        else fire.enableEmission = false;
    }

    private void ClampFuel()
    {
        if (playerStats.fuel > 5) playerStats.fuel = _maxFuel;
    }

    private void ManageFuel()
    {
        var fuelNormalized = playerStats.fuel / _maxFuel;

        progress.fillAmount = fuelNormalized;
    }
}
