using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    public PlayerStats playerStats;
    public FadeScreen fadeScreen;
    public BallBreaker ballBreaker;
    public Player superGlassBall;
    public PowerSlam powerSlam;
    public PowerSpeed powerSpeed;
    public float timeToRespawn = 3;

    private Vector3 _fixPosition;
    private float _timeToRebuild = 3.2f;
    private bool _respawnActivated;
    private bool _rebuildActivated;

    public Vector3 FixPosition
    {
        get { return _fixPosition; }
        set { _fixPosition = value; }
    }
	
    public void Start ()
    {
        FixPosition = new Vector3(0, 10, 0);
    }

	public void Update ()
    {
        Respawn();
        Die();
	}

    public void Respawn()
    {
        UseLife();

        if (_respawnActivated) timeToRespawn -= Time.deltaTime;

        if (timeToRespawn <= 0)
        {
            if(playerStats.lives >= 0)
            {
                if (!_rebuildActivated)
                {
                    powerSlam.ResetAll();
                    powerSpeed.ResetAll();

                    ballBreaker.SuperGlassBallBreak03.transform.position = playerStats.lastCheckpoint.transform.position + FixPosition;
                    foreach (Transform child in ballBreaker.SuperGlassBallBreak03.transform) child.GetComponentInChildren<PlayerRebuild>().StartDrift();
                    _rebuildActivated = true;
                }

                if (_rebuildActivated) _timeToRebuild -= Time.deltaTime;

                if (_timeToRebuild <= 0)
                {
                    Destroy(ballBreaker.SuperGlassBallBreak03);
                    playerStats.CurrentHealth = playerStats.maxHealth;
                    superGlassBall.gameObject.SetActive(true);
                    superGlassBall.transform.position = playerStats.lastCheckpoint.transform.position;
                    superGlassBall.rb.velocity = Vector3.zero;
                    superGlassBall.rb.angularDrag = 0;
                    timeToRespawn = 3;
                    _timeToRebuild = 3.2f;
                    _respawnActivated = false;
                    _rebuildActivated = false;
                }
            }
        }
    }

    public void Die()
    {
        if (playerStats.lives < 0 && playerStats.CurrentHealth <= 0)
        {
            if (!playerStats.dead)
            {
                StartCoroutine(GoToMenu());
                playerStats.dead = true;
            }
        }
    }

    private void UseLife()
    {
        if (playerStats.CurrentHealth <= 0 && !_respawnActivated)
        {
            playerStats.lives--;
            _respawnActivated = true;
        }
    }

    private IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(4f);
        fadeScreen.SetNextScene("Menu");
        StopCoroutine(GoToMenu());
    }
}
