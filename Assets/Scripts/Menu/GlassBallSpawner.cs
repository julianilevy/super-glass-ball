
using UnityEngine;
using System.Collections;

public class GlassBallSpawner : MonoBehaviour
{
    public Waypoint[] waypoints;
    public GameObject glassBall;
    public GameObject metalBall;
    public GameObject collectable;
    public float spawnTime;
    public float speed;
    public int maxBalls;

    private Vector3 _direction;
    private float _spawnTimer;
    private int _ballsSpawned;
    private int _currentWaypoint = 1;

    private void Start()
    {
        _direction = Vector3.right;
    }

	private void Update ()
    {
        WaypointsMovement();
        SpawnObjects();
	}

    private void SpawnObjects()
    {
        if (_ballsSpawned < maxBalls)
        {
            _spawnTimer += Time.deltaTime;

            if (_spawnTimer >= spawnTime)
            {
                var randomSpawn = Random.Range(0, 3);
                GameObject futureSpawn = null;

                if (randomSpawn == 0) futureSpawn = glassBall;
                if (randomSpawn == 1) futureSpawn = metalBall;
                if (randomSpawn == 2) futureSpawn = collectable;

                var randomXForce = Random.Range(-1, 1);
                var randomZForce = Random.Range(-1, 1);

                var spawnedObject = Instantiate(futureSpawn, transform.position, transform.rotation) as GameObject;
                spawnedObject.GetComponent<Rigidbody>().AddForce(randomXForce * 10000, -60000, randomZForce * 10000);
                _ballsSpawned++;
                _spawnTimer = 0;
            }
        }
    }

    private void WaypointsMovement()
    {
        if (waypoints.Length > 0)
        {
            transform.position += _direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, waypoints[_currentWaypoint].transform.position) <= 5)
            {
                _direction *= -1;

                if (_currentWaypoint < waypoints.Length - 1) _currentWaypoint++;
                else _currentWaypoint = 0;
            }
        }
    }
}
