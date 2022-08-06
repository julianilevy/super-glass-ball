using UnityEngine;
using System.Collections;

public class GrabPower : MonoBehaviour
{
    public Powers powers;

    private Player _player;
    private float _timeToDestroy = 3f;
    private bool _activateFuel;
    private bool _grabbed;

    public enum Powers
    {
        PowerSlam,
        PowerSpeed
    }

	private void Update ()
    {
        Rotation();
        Scale();
        AddFuel();
	}

    private void Rotation()
    {
        transform.Rotate(Vector3.up, 3f);
    }

    private void Scale()
    {
        if (_grabbed)
        {
            _timeToDestroy -= Time.deltaTime;
            transform.localScale -= Vector3.one * Time.deltaTime / 3;
            gameObject.GetComponent<ParticleSystem>().startSize -= Time.deltaTime * 3.2f;
        }

        if (_timeToDestroy <= 0.1f)
        {
            gameObject.GetComponent<ParticleSystem>().enableEmission = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(this.gameObject, 1);
        } 
    }

    private void AddFuel()
    {
        if (_activateFuel)
        {
            if (_player.playerStats.fuel < 5)
            {
                _player.playerStats.fuel += Time.deltaTime * 2;
                _player.playerStats.fullingFuel = true;
            } 
            if (_player.playerStats.fuel > 5)
            {
                _player.playerStats.fuel = 5;
                _player.playerStats.fullingFuel = false;
            } 
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!_grabbed)
        {
            if (collider.gameObject.layer == K.LAYER_PLAYER)
            {
                if (powers == Powers.PowerSlam)
                {
                    _player = collider.GetComponent<Player>();
                    _player.playerStats.gotPowerSlam = true;
                    _player.playerStats.ammo = 1;
                }
                if (powers == Powers.PowerSpeed)
                {
                    _player = collider.GetComponent<Player>();
                    _player.playerStats.gotPowerSpeed = true;
                    _activateFuel = true;
                }

                _grabbed = true;
                _player.rb.velocity = Vector3.zero;
                _player.rb.angularVelocity = Vector3.zero;
            }
        }
    }
}