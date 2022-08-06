using UnityEngine;
using System.Collections;

public class BallMeshManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public Player superMetalBall;
    public Player superGlassBall;
    public Player superGlassBallBreak01;
    public Player superGlassBallBreak02;
    public GameObject superGlassBallBreak03Prefab;

    protected Player[] allBallsMeshes = new Player[4];
    protected Player lastBallPlayer;
    protected GameObject lastBallGO;
    private GameObject _superGlassBallBreak03;

    public GameObject SuperGlassBallBreak03
    {
        get { return _superGlassBallBreak03; }
        set { _superGlassBallBreak03 = value; }
    }

    public virtual void Start()
    {
        AddBallMeshes(superMetalBall, 0);
        AddBallMeshes(superGlassBall, 1);
        AddBallMeshes(superGlassBallBreak01, 2);
        AddBallMeshes(superGlassBallBreak02, 3);
    }

    protected void MeshChangeFunction(Player ballFromPool)
    {
        foreach(var ballMesh in allBallsMeshes)
        {
            if (ballMesh.gameObject.activeSelf)
            {
                lastBallPlayer = ballMesh.GetComponent<Player>();
                ballMesh.Grounded = false;
                ballMesh.gameObject.SetActive(false);
            }
        }

        ballFromPool.transform.position = lastBallPlayer.transform.position;
        if (ballFromPool != superMetalBall)
        {
            ballFromPool.rb.velocity = lastBallPlayer.rb.velocity;
            ballFromPool.rb.angularVelocity = lastBallPlayer.rb.angularVelocity;
        }
        ballFromPool.gameObject.SetActive(true);
    }

    protected void MeshChangeFunction(GameObject ballFromPool)
    {
        foreach (var ballMesh in allBallsMeshes)
        {
            if (ballMesh.gameObject.activeSelf)
            {
                lastBallGO = ballMesh.gameObject;
                ballMesh.gameObject.SetActive(false);
            }
        }

        SuperGlassBallBreak03 = Instantiate(ballFromPool, lastBallGO.transform.position, ballFromPool.transform.rotation) as GameObject;
        ballFromPool.gameObject.SetActive(true);
    }

    private void AddBallMeshes(Player ballMesh, int index)
    {
        allBallsMeshes[index] = ballMesh;
    }
}