using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class German : Enemy
{
    public GameDataManager gameDataManager;
    public PlayerStats playerStats;
    public GermanQuiz germanQuiz;
    public GameObject blackCurtain;

    private bool _arrived;
    private bool _quizDone;

    public bool Arrived
    {
        get { return _arrived; }
        set { _arrived = value; }
    }

    public bool QuizDone
    {
        get { return _quizDone; }
        set { _quizDone = value; }
    }

    public override void Start()
    {
        if (gameDataManager.quizDisabled)
        {
            Destroy(blackCurtain.gameObject);
            Destroy(this.transform.parent.gameObject);
            return;
        }

        base.Start();
        playerStats.locked = true;
    }

    public override void FixedUpdate()
    {
        if (!Dead)
        {
            WaypointsMovementGerman();
            Die();
        }
    }

    public void WaypointsMovementGerman()
    {
        if (waypoints.Length > 0)
        {
            if(!_arrived)
            {
                var dirToWaypoint = waypoints[currentWaypoint].transform.position - transform.position;
                dirToWaypoint.y = transform.forward.y;

                transform.forward = Vector3.Slerp(transform.forward, dirToWaypoint, Time.deltaTime);
                transform.position += transform.forward * (speed * 4) * Time.deltaTime;

                if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) <= 5f)
                {
                    if (currentWaypoint < waypoints.Length - 1) currentWaypoint++;
                    else currentWaypoint = 0;

                    if (currentWaypoint >= 5)
                    {
                        if (!QuizDone) StartCoroutine(germanQuiz.ActivatePresentationText());
                    }
                }
            }
        }
    }

    public void SetMaterial(Material material)
    {
        gameObject.GetComponentInChildren<Renderer>().material = material;
    }
}