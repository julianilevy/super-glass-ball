using UnityEngine;
using System.Collections;

public class RestoreEnemy : MonoBehaviour
{
    public Enemy enemyPrefab;
    public RestoreEnemyAux restoreAuxPrefab;

    private Enemy _enemy;
    private Vector3 _enemyPos;
    private Quaternion _enemyRot;
    private float _timeToRestore = 3;
    private float _restoreTimer;
    private bool _targetSeen;
    private bool _restoreOn;
    private bool _restored;

    private void Start()
    {
        enemyPrefab = GetComponent<Enemy>();
        _enemy = GetComponent<Enemy>();
        _enemyPos = gameObject.transform.position;
        _enemyRot = gameObject.transform.rotation;
    }

    private void Update()
    {
        ActivateRestoration();
    }

    private void ActivateRestoration()
    {
        if (!_enemy.Dead || _enemy.deadRespawn)
        {
            if (_enemy.targetInSight) _targetSeen = true;
            if (!_enemy.targetInSight && _targetSeen) _restoreOn = true;
            if (_restoreOn) _restoreTimer += Time.deltaTime;
            if (_restoreTimer >= _timeToRestore) RestoreNoDeath();
        }
        if (_enemy.Dead && _enemy.deadRespawn) Restore();
    }

    public void RestoreNoDeath()
    {
        var newEnemy = Instantiate(enemyPrefab.gameObject, _enemyPos, _enemyRot) as GameObject;
        newEnemy.GetComponent<Enemy>().Dead = false;
        _restoreOn = false;
        _restoreTimer = 0;
        Destroy(this.gameObject);
    }

    public void Restore()
    {
        var newEnemy = Instantiate(enemyPrefab.gameObject, _enemyPos, _enemyRot) as GameObject;
        newEnemy.GetComponent<Enemy>().Dead = false;
        newEnemy.SetActive(false);
        var restoreAux = Instantiate(restoreAuxPrefab.gameObject, _enemyPos, _enemyRot) as GameObject;
        restoreAux.GetComponent<RestoreEnemyAux>().SetEnemy(newEnemy);
        _restoreOn = false;
        _restoreTimer = 0;
        Destroy(this.gameObject);
    }
}