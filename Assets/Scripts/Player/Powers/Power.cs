using UnityEngine;
using System.Collections;

public class Power : BallMeshManager
{
    protected bool toMetalDone;
    protected bool toGlassDone;

    public void ChangeMeshToMetal()
    {
        MeshChangeFunction(superMetalBall);
    }

    public void ChangeMeshToGlass()
    {
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

    public void ActivatePowerSlam()
    {
        if(playerStats.ammo > 0)
        {
            playerStats.powered = true;
            playerStats.powerSlamON = true;
            superMetalBall.GetComponent<ParticleSystem>().enableEmission = false;
        }
    }

    public void ActivatePowerSpeed()
    {
        if(playerStats.fuel >= 2.5f)
        {
            playerStats.powered = true;
            playerStats.powerSpeedON = true;
            superMetalBall.GetComponent<ParticleSystem>().enableEmission = false;
        }
    }

    public void DeactivatePowerSlam()
    {
        playerStats.powered = false;
        playerStats.powerSlamON = false;
    }

    public void DeactivatePowerSpeed()
    {
        playerStats.powered = false;
        playerStats.powerSpeedON = false;
    }
}