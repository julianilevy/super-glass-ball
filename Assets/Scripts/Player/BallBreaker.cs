using UnityEngine;
using System.Collections;

public class BallBreaker : BallMeshManager
{
    private int _health;

    public override void Start()
    {
        base.Start();

        _health = playerStats.CurrentHealth;
    }

	public void Update ()
    {
        BreakBall();
	}

    public void BreakBall()
    {
        if (_health != playerStats.CurrentHealth)
        {
            _health = playerStats.CurrentHealth;

            switch (playerStats.CurrentHealth)
            {
                case 3:
                    MeshChangeFunction(superGlassBall);
                    break;
                case 2:
                    MeshChangeFunction(superGlassBallBreak01);
                    break;
                case 1:
                    MeshChangeFunction(superGlassBallBreak02);
                    break;
                case 0:
                    MeshChangeFunction(superGlassBallBreak03Prefab);
                    break;
            }

            if (playerStats.CurrentHealth < 0) MeshChangeFunction(superGlassBallBreak03Prefab);
        }
    }
}