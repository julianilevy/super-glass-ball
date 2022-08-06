using UnityEngine;
using System.Collections;
using System.IO;

public class LevelsMovement : MonoBehaviour
{
    public GameDataManager gameDataManager;

    private Vector3 _initialPos;
    private Vector3 _finalPos;
    private float _timeToMove;
    private bool _active;

    private void Start()
    {
        if ((File.Exists(Application.persistentDataPath + "/Menu.sav")))
        {
            transform.position = new Vector3(gameDataManager.menuLevelPositionX, gameDataManager.menuLevelPositionY, gameDataManager.menuLevelPositionZ);
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_active)
        {
            _timeToMove += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(_initialPos, _finalPos, _timeToMove);

            if (_timeToMove >= 1)
            {
                gameDataManager.menuLevelPositionX = transform.position.x;
                gameDataManager.menuLevelPositionY = transform.position.y;
                gameDataManager.menuLevelPositionZ = transform.position.z;
                if (!gameDataManager.cheatedLevels) gameDataManager.Save();
                _timeToMove = 0;
                _active = false;
            }
        }
    }

    public void MakeMove()
    {
        _active = true;
    }

    public void SetInitialPos(float initialPos)
    {
        _initialPos = new Vector3(initialPos, transform.position.y, transform.position.z);
    }

    public void SetFinalPos(float finalPos)
    {
        _finalPos = new Vector3(finalPos, transform.position.y, transform.position.z);
        MakeMove();
    }
}