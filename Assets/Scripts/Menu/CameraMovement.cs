using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public float finalPosX;
    public float finalPosY;

    private Vector3 _initialPos;
    private Vector3 _finalPos;
    private float _timeToMove;
    private float _timeToBack;
    private bool _active;
    private bool _moved;
    private bool _readyToBack;

    private void Start()
    {
        _initialPos = transform.position;
    }

    private void Update()
    {
        Move();
        Back();
    }

    private void Move()
    {
        if (_active)
        {
            if (!_moved)
            {
                _timeToMove += Time.deltaTime;
                transform.localPosition = Vector3.Lerp(_initialPos, _finalPos, _timeToMove);

                if (_timeToMove >= 1)
                {
                    _timeToMove = 0;
                    _moved = true;
                }
            }
        }
    }

    private void Back()
    {
        if (_readyToBack)
        {
            _timeToBack += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(_finalPos, _initialPos, _timeToBack);

            if (_timeToBack >= 1)
            {
                _active = false;
                _moved = false;
                _readyToBack = false;
                _timeToBack = 0;
            }
        }
    }

    public void MakeMove()
    {
        _active = true;
    }

    public void MakeBack()
    {
        _readyToBack = true;
    }

    public void SetFinalPos(string move)
    {
        if (move == "X") _finalPos = new Vector3(finalPosX, transform.position.y, transform.position.z);
        if (move == "Y") _finalPos = new Vector3(transform.position.x, finalPosY, transform.position.z);
    }
}