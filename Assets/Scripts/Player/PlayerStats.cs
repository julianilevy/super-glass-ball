using UnityEngine;
using System.Collections;

public class PlayerStats : Entity
{
    public IPower[] powers = new IPower[2];
    public Power powerSlam;
    public Power powerSpeed;
    public Checkpoint lastCheckpoint;
    public float jumpForce;
    public float rotationSpeed;
    public float fuel;
    public int lives;
    public int collectables;
    public int ammo;
    public bool locked;
    public bool dead;
    public bool fullingFuel;
    public bool running;
    public bool powered;
    public bool powerSlamON;
    public bool powerSpeedON;
    public bool gotPowerSlam;
    public bool gotPowerSpeed;
    public bool levelCompleted;

    private IPower _powerSlam;
    private IPower _powerSpeed;

    public override void Start()
    {
        base.Start();

        _powerSlam = powerSlam.GetComponent<IPower>();
        _powerSpeed = powerSpeed.GetComponent<IPower>();

        AddPower(_powerSlam, K.POWERARRAY_SLAM);
        AddPower(_powerSpeed, K.POWERARRAY_SPEED);
    }

    public void AddPower(IPower power, int index)
    {
        powers[index] = power;
    }
}