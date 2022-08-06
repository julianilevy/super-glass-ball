using UnityEngine;
using System.Collections;

public class GermanEnemySpawner : MonoBehaviour
{
    public Player superGlassBall;
    public Player superGlassBallBreak01;
    public Player superGlassBallBreak02;
    public GermanSpikedEnemy germanSpikedEnemy;

    public void SpawnEnemy()
    {
        var newEnemyGO = Instantiate(germanSpikedEnemy.gameObject, transform.position, transform.rotation) as GameObject;
        var newEnemy = newEnemyGO.GetComponent<GermanSpikedEnemy>();
        newEnemy.superGlassBall = superGlassBall;
        newEnemy.superGlassBallBreak01 = superGlassBallBreak01;
        newEnemy.superGlassBallBreak02 = superGlassBallBreak02;
    }
}
