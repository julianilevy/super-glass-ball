using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerSlamText : MonoBehaviour
{
    public PlayerStats playerStats;
    public int totalAmmo;

    private MoveTexts _parent;
    private Text _text;
    private float _updateAmmoTimer;
    private int _currentAmmo;

    private void Start()
    {
        _parent = gameObject.GetComponentInParent<MoveTexts>();
        _text = gameObject.GetComponent<Text>();
        _text.text = "00 / 0" + totalAmmo;
    }

    private void Update()
    {
        ShowAmmo();
    }

    private void ShowAmmo()
    {
        if (_currentAmmo != playerStats.ammo) _parent.active = true;

        if (_parent.active)
        {
            _updateAmmoTimer += Time.deltaTime;

            if (_currentAmmo != playerStats.ammo)
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

            if (_updateAmmoTimer >= 1.4f) _currentAmmo = playerStats.ammo;
        }
        else _updateAmmoTimer = 0;

        _text.text = "0" + _currentAmmo + " / 0" + totalAmmo;
    }
}