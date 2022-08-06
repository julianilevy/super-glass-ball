using UnityEngine;
using System.Collections;

public class RestoreEnemyAux : MonoBehaviour
{
    public float respawnTimer;

    private GameObject enemy;

	private void Update ()
    {
        ActivateNewEnemy();
	}

    private void ActivateNewEnemy()
    {
        respawnTimer -= Time.deltaTime;

        if (respawnTimer <= 0)
        {
            enemy.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    public void SetEnemy(GameObject enemyRef)
    {
        enemy = enemyRef;
    }
}