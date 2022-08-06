using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    public PlayerStats playerStats;
    public PlayerRespawn playerRespawner;
    public BallBreaker ballBreaker;
    public Player superMetalBall;
    public Player superGlassBall;
    public Player superGlassBallBreak01;
    public Player superGlassBallBreak02;

    private Player[] _allBallsMeshes = new Player[4];
    private Player _currentBall;
    private Vector3 _offset;

    private void Start()
    {
        AddBallMeshes(superMetalBall, 0);
        AddBallMeshes(superGlassBall, 1);
        AddBallMeshes(superGlassBallBreak01, 2);
        AddBallMeshes(superGlassBallBreak02, 3);

        _currentBall = superGlassBall;
        _offset = transform.position - _currentBall.transform.position;
    }

	private void Update ()
    {
        SetCamera();
	}

    private void SetCamera()
    {
        foreach (var ballMesh in _allBallsMeshes)
        {
            if (ballMesh.gameObject.activeSelf)
            {
                _currentBall = ballMesh;
            }
        }

        if(playerStats.lives >= 0)
        {
            if (playerStats.CurrentHealth > 0) transform.position = _currentBall.transform.position + _offset;
            else
            {
                if (playerRespawner.timeToRespawn < 0) transform.position = ballBreaker.SuperGlassBallBreak03.transform.position - playerRespawner.FixPosition + _offset;
            }
        }
    }

    private void AddBallMeshes(Player ballMesh, int index)
    {
        _allBallsMeshes[index] = ballMesh;
    }
}