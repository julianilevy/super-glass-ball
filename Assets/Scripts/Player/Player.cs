using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public PlayerStats playerStats;
    public Rigidbody rb;
    public float radius = 1.2f;

    private float _extraJump;
    private float _moveX;
    private float _moveZ;
    private bool _grounded;
    private bool _lastHitBreakable;
    private bool _superJumpEnabled;

    public float ExtraJump
    {
        get { return _extraJump; }
        set { _extraJump = value; }
    }

    public bool Grounded
    {
        get { return _grounded; }
        set { _grounded = value; }
    }

    public bool LastHitBreakable
    {
        get { return _lastHitBreakable; }
        set { _lastHitBreakable = value; }
    }

    public bool SuperJumpEnabled
    {
        get { return _superJumpEnabled; }
        set { _superJumpEnabled = value; }
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
        PowerKeys();
        CheckCollisions();
    }

    public void Move()
    {
        if(!playerStats.locked)
        {
            _moveX = Input.GetAxis("Horizontal");
            _moveZ = Input.GetAxis("Vertical");

            if (playerStats.running) _moveX = Input.GetAxis("Horizontal") * playerStats.rotationSpeed;
            if (playerStats.powerSpeedON) _moveZ = Mathf.Clamp(_moveZ, 1f, 1f);

            if (!playerStats.running)
            {
                Vector3 movement = new Vector3(_moveX, 0f, _moveZ);
                rb.AddForce(movement * playerStats.speed);
            }
            else
            {
                Vector3 movement = new Vector3(_moveX, 0f, 0f);
                rb.AddForce(movement);
            }

            if (Input.GetKey(KeyCode.Space) && Grounded && !playerStats.powerSlamON)
            {
                if (!SuperJumpEnabled) rb.AddForce(new Vector3(0, 1, 0) * (playerStats.jumpForce * 100));
                else
                {
                    rb.AddForce(new Vector3(0, 1, 0) * (playerStats.jumpForce * ExtraJump));
                    SuperJumpEnabled = false;
                }
                Grounded = false;
            }
        }
        if(playerStats.locked)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void PowerKeys()
    {
        if (!playerStats.locked)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && playerStats.gotPowerSlam && !playerStats.powered) playerStats.powers[K.POWERARRAY_SLAM].TriggerPower();
            if (Input.GetKeyDown(KeyCode.Alpha2) && playerStats.gotPowerSpeed && !playerStats.powered) playerStats.powers[K.POWERARRAY_SPEED].TriggerPower();
        }
    }

    public void CheckCollisions()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var collider in hitColliders)
        {
            if (collider.gameObject.layer == K.LAYER_ENEMY)
            {
                SpikedEnemy hitSpikedEnemy;
                hitSpikedEnemy = collider.GetComponentInParent<SpikedEnemy>();

                if (hitSpikedEnemy is SpikedEnemy)
                {
                    if (playerStats.powered) hitSpikedEnemy.TakeDamage(playerStats.damage);
                    if (!playerStats.powered && !hitSpikedEnemy.Dead) playerStats.TakeDamage(hitSpikedEnemy.damage);
                }

                GermanSpikedEnemy hitGermanSpikedEnemy;
                hitGermanSpikedEnemy = collider.GetComponentInParent<GermanSpikedEnemy>();

                if (hitGermanSpikedEnemy is GermanSpikedEnemy)
                {
                    if (playerStats.powered) hitGermanSpikedEnemy.TakeDamage(playerStats.damage);
                    if (!playerStats.powered && !hitGermanSpikedEnemy.Dead) playerStats.TakeDamage(hitGermanSpikedEnemy.damage);
                }

                ShooterEnemy hitShooterEnemy;
                hitShooterEnemy = collider.GetComponentInParent<ShooterEnemy>();

                if (hitShooterEnemy is ShooterEnemy)
                {
                    if (playerStats.powered) hitShooterEnemy.TakeDamage(playerStats.damage);
                }

                RealGerman hitRealGerman;
                hitRealGerman = collider.GetComponentInParent<RealGerman>();

                if (hitRealGerman is RealGerman)
                {
                    if (!playerStats.powered && !hitRealGerman.Dead) playerStats.TakeDamage(hitRealGerman.damage);
                }
            }

            if (collider.gameObject.layer == K.LAYER_FLOOR)
            {
                LavaFloor hitLavaFloor;
                hitLavaFloor = collider.GetComponent<LavaFloor>();

                if (hitLavaFloor is LavaFloor)
                {
                    if (!playerStats.powered) if (rb.velocity.magnitude <= 6) playerStats.TakeDamage(1);
                }
            }

            if (collider.gameObject.layer == K.LAYER_HARMFULOBJECT) if (!playerStats.powered) playerStats.TakeDamage(1);
            if (collider.gameObject.layer == K.LAYER_DESTROYEROBJECT) if (!playerStats.powered) playerStats.TakeDamage(3);
            if (collider.gameObject.layer == K.LAYER_MAPLIMIT) playerStats.TakeDamage(3);
        }

        Collider[] hitCollidersBig = Physics.OverlapSphere(transform.position, radius * 2.5f);

        foreach (var collider in hitCollidersBig)
        {
            if (collider.gameObject.layer == K.LAYER_GLASS)
            {
                Breakable hitBreakable;
                hitBreakable = collider.GetComponent<Breakable>();

                if (hitBreakable is Breakable) if (playerStats.powered) hitBreakable.BreakGlass();
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == K.LAYER_FLOOR)
        {
            Grounded = true;
            LastHitBreakable = false;
        }
        if (collision.gameObject.layer == K.LAYER_ENEMY) Grounded = true;
        if (collision.gameObject.layer == K.LAYER_HARMFULOBJECT) Grounded = true;
    }
}