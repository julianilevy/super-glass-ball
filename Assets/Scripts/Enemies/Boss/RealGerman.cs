using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RealGerman : Enemy
{
    public GameDataManager gameDataManager;
    public PlayerStats playerStats;
    public GermanFireball fireball;
    public Renderer germanRenderer;
    public ParticleSystem particleFire;
    public ParticleSystem particleSteam;
    public Transform fireballSpawner;
    public GermanEnemySpawner enemySpawner01;
    public GermanEnemySpawner enemySpawner02;
    public Transform ammoSpawner01;
    public Transform ammoSpawner02;
    public Transform ammoSpawner03;
    public Transform ammoSpawner04;
    public Transform ammoSpawner05;
    public GrabAmmoFuel grabAmmo;
    public GermanGlassWall germanGlassWall;
    public FinalText finalText;
    public FadeScreen fadeScreen;

    private Material _currentMaterial;
    private float _spawnAmmoTimer;
    private float _attackCD;
    private float _onDashTimer;
    private float _shootTimer;
    private float _glassWallYPos;
    private int _health;
    private int _shootQty;
    private bool _onAttack;
    private bool _onDash;
    private bool _onDashReady;
    private bool _onShooting;
    private bool _enemiesSpawned;
    private bool _glassWallSpawned;
    private bool _dying;
    private bool _readyToFight;
    private bool _readyToDisappear;

    public bool ReadyToFight
    {
        get { return _readyToFight; }
        set { _readyToFight = value; }
    }

    public bool ReadyToDisappear
    {
        get { return _readyToDisappear; }
        set { _readyToDisappear = value; }
    }

    public override void Start()
    {
        base.Start();
        _currentMaterial = germanRenderer.material;
        _currentMaterial.color = Color.white;
        germanRenderer.material = _currentMaterial;
        _glassWallYPos = 147.43f;
        _health = CurrentHealth;
    }

    public override void FixedUpdate()
    {
        LookAtPlayer();
        Disappear();

        if (!Dead)
        {
            WaypointsMovementGerman();

            if (!_dying)
            {
                UpdateTargetReference();
                Detection();
                CheckFarness();
                ExplosionHit();
                ReceiveDamage();

                if (_readyToFight)
                {
                    CheckGlassCollisions();
                    SpawnAmmo();
                    Die();
                    MakeAttack();
                    Dash();
                    Shoot();
                }
            }
        }
    }

    public void WaypointsMovementGerman()
    {
        if (_readyToFight)
        {
            if (!_onDashReady)
            {
                if (waypoints.Length > 0)
                {
                    var dirToWaypoint = waypoints[currentWaypoint].transform.position - transform.position;
                    dirToWaypoint.y = transform.forward.y;

                    transform.forward = Vector3.Slerp(transform.forward, dirToWaypoint, Time.deltaTime * speed);
                    transform.position += transform.forward * (speed * 4) * Time.deltaTime;

                    if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) <= 2.5f)
                    {
                        if (currentWaypoint < waypoints.Length - 1) currentWaypoint++;
                        else currentWaypoint = 0;
                    }

                    if (_dying)
                    {
                        if (currentWaypoint == 1)
                        {
                            Dead = true;
                            StartCoroutine(finalText.ActivateTrueFinalVictoryText());
                        }
                    }
                }
            }
        }
    }

    public void CheckGlassCollisions()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 4.5f);

        foreach (var collider in hitColliders)
        {
            if (collider.gameObject.layer == K.LAYER_GLASS)
            {
                Breakable hitBreakable;
                hitBreakable = collider.GetComponent<Breakable>();
                if (hitBreakable is Breakable) hitBreakable.BreakGlass();
            }
        }
    }

    public void MakeAttack()
    {
        if (!_onAttack) _attackCD += Time.deltaTime;

        if (!_enemiesSpawned && _attackCD >= 5)
        {
            if (CurrentHealth <= 3000)
            {
                if (!_glassWallSpawned) SpawnGlass();
            }

            var decideEnemySpawn = Random.Range(1, 3);
            if (decideEnemySpawn == 2) SpawnEnemies();
            _enemiesSpawned = true;
        }

        if (_attackCD >= 10)
        {
            var randomAttack = Random.Range(1, 3);

            if (randomAttack == 1)
            {
                _onAttack = true;
                _onDash = true;
                _enemiesSpawned = false;
                _attackCD = 0;
            }
            if (randomAttack == 2)
            {
                _onAttack = true;
                _onShooting = true;
                _enemiesSpawned = false;
                _attackCD = 0;
            }
        }
    }

    public void Dash()
    {
        if (_onDash)
        {
            if (currentWaypoint == 0 || currentWaypoint == 1 || currentWaypoint == 2 || currentWaypoint == 5 || currentWaypoint == 6 || currentWaypoint == 7)
            {
                if (!_onDashReady) _onDashReady = true;
            }

            if (_onDashReady)
            {
                _onDashTimer += Time.deltaTime;

                if (CurrentHealth <= 3000)
                {
                    if (!_glassWallSpawned)
                    {
                        SpawnGlass();
                        _glassWallSpawned = true;
                    }
                }

                if (_onDashTimer < 2)
                {
                    var dirToTarget = targetReference.transform.position - transform.position;
                    dirToTarget.y = transform.forward.y;

                    transform.forward = Vector3.Slerp(transform.forward, dirToTarget, Time.deltaTime * speed);
                }
                if (_onDashTimer >= 2 && _onDashTimer < 4) transform.position += transform.forward * (speed * 10) * Time.deltaTime;
                if (_onDashTimer >= 4)
                {
                    Collider[] hitColliders = Physics.OverlapSphere(transform.position, 18);

                    foreach (var collider in hitColliders)
                    {
                        if (collider.gameObject.layer == K.LAYER_WAYPOINT)
                        {
                            Waypoint hitWaypoint;
                            hitWaypoint = collider.GetComponent<Waypoint>();

                            if (hitWaypoint is Waypoint)
                            {
                                for (int i = 0; i < waypoints.Length; i++)
                                {
                                    if (hitWaypoint.gameObject == waypoints[i].gameObject)
                                    {
                                        if (i != 9) currentWaypoint = i + 1;
                                        else currentWaypoint = 0;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    _onAttack = false;
                    _onDash = false;
                    _onDashReady = false;
                    _glassWallSpawned = false;
                    _onDashTimer = 0;
                }
            }
        }
    }

    public void Shoot()
    {
        if(_onShooting)
        {
            _shootTimer += Time.deltaTime;

            if(CurrentHealth <= 3000)
            {
                if(!_glassWallSpawned)
                {
                    SpawnGlass();
                    _glassWallSpawned = true;
                }
            }

            if (_shootTimer >= 0.75f)
            {
                var newFireballGO = Instantiate(fireball.gameObject, fireballSpawner.position, fireballSpawner.rotation) as GameObject;
                var newFireball = newFireballGO.GetComponent<GermanFireball>();
                newFireball.SetTarget(targetReference.transform);
                if (CurrentHealth <= 3000) newFireball.SetSpeedDivider(1);
                else newFireball.SetSpeedDivider(2);
                _shootQty++;
                _shootTimer = 0;
            }
            if (_shootQty >= 20)
            {
                _onAttack = false;
                _onShooting = false;
                _glassWallSpawned = false;
                _shootTimer = 0;
                _shootQty = 0;
            }
        }
    }

    public void SpawnEnemies()
    {
        enemySpawner01.SpawnEnemy();
        enemySpawner02.SpawnEnemy();
    }

    public void SpawnGlass()
    {
        var spawnPos = new Vector3(targetReference.transform.position.x, _glassWallYPos, targetReference.transform.position.z);

        Instantiate(germanGlassWall.gameObject, spawnPos, germanGlassWall.transform.rotation);
    }

    public void SpawnAmmo()
    {
        _spawnAmmoTimer += Time.deltaTime;

        if (_spawnAmmoTimer >= 40)
        {
            var randomSpawnPoint = Random.Range(1, 6);

            if (randomSpawnPoint == 1) Instantiate(grabAmmo.gameObject, ammoSpawner01.position, grabAmmo.transform.rotation);
            if (randomSpawnPoint == 2) Instantiate(grabAmmo.gameObject, ammoSpawner02.position, grabAmmo.transform.rotation);
            if (randomSpawnPoint == 3) Instantiate(grabAmmo.gameObject, ammoSpawner03.position, grabAmmo.transform.rotation);
            if (randomSpawnPoint == 4) Instantiate(grabAmmo.gameObject, ammoSpawner04.position, grabAmmo.transform.rotation);
            if (randomSpawnPoint == 5) Instantiate(grabAmmo.gameObject, ammoSpawner05.position, grabAmmo.transform.rotation);

            _spawnAmmoTimer = 0;
        }
    }

    protected override void Die()
    {
        if (currentHealth <= 0)
        {
            _dying = true;
            playerStats.levelCompleted = true;
            gameDataManager.completedLevelBossGold = true;
            gameDataManager.Save();
        }
    }

    public void LookAtPlayer()
    {
        if (Dead)
        {
            var dirToTarget = targetReference.transform.position - transform.position;
            dirToTarget.y = transform.forward.y;

            transform.forward = Vector3.Slerp(transform.forward, dirToTarget, Time.deltaTime * speed);
        }
    }

    public void Disappear()
    {
        if (_readyToDisappear)
        {
            _currentMaterial.color -= new Color(0, 0, 0, 0.01f);

            if (_currentMaterial.color.a <= 0)
            {
                _currentMaterial.color = new Color(255, 255, 255, 0);
                _readyToDisappear = false;
                StartCoroutine(FinishGame());
            }
        }
    }

    public void ReceiveDamage()
    {
        if(_health != CurrentHealth)
        {
            StartCoroutine(ChangeColorWhenHurt());
            _health = CurrentHealth;
        }
    }

    public IEnumerator ChangeColorWhenHurt()
    {
        _currentMaterial.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _currentMaterial.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        _currentMaterial.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _currentMaterial.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        _currentMaterial.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _currentMaterial.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        _currentMaterial.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _currentMaterial.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        _currentMaterial.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _currentMaterial.color = Color.white;

        _currentMaterial.color = Color.white;
        germanRenderer.material = _currentMaterial;

        StopCoroutine(ChangeColorWhenHurt());
    }

    public IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(4f);
        fadeScreen.ActivateFade();
        StopCoroutine(FinishGame());
    }
}