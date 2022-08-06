using UnityEngine;
using System.Collections;

public class GermanMoveTexts : MonoBehaviour
{
    public AxisMove axisMove;
    public float finalPosX;
    public float finalPosY;
    public bool active;

    private Vector3 _initialPos;
    private Vector3 _finalPos;
    private float _timeToAppear;
    private float _timeToDisappear;
    private bool _appeared;
    private bool _readyToDisappear;

    public enum AxisMove
    {
        X,
        Y
    }

    public float TimeToDisappear
    {
        get { return _timeToDisappear; }
        set { _timeToDisappear = value; }
    }

    public bool ReadyToDisappear
    {
        get { return _readyToDisappear; }
        set { _readyToDisappear = value; }
    }

	private void Start ()
    {
        _initialPos = transform.localPosition;
        if (axisMove == AxisMove.X) _finalPos = new Vector3(finalPosX, transform.localPosition.y, transform.localPosition.z);
        if (axisMove == AxisMove.Y) _finalPos = new Vector3(transform.localPosition.x, finalPosY, transform.localPosition.z);
	}
	
	private void FixedUpdate ()
    {
        Appear();
        Disappear();
	}

    private void Appear()
    {
        if (active)
        {
            if (!_appeared)
            {
                _timeToAppear += Time.deltaTime;
                transform.localPosition = Vector3.Lerp(_initialPos, _finalPos, _timeToAppear);

                if (_timeToAppear >= 1)
                {
                    _timeToAppear = 0;
                    _appeared = true;
                }
            }
        }
    }

    private void Disappear()
    {
        if (_readyToDisappear)
        {
            _timeToDisappear += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(_finalPos, _initialPos, _timeToDisappear);

            if (_timeToDisappear >= 1)
            {
                active = false;
                _appeared = false;
                _readyToDisappear = false;
                _timeToDisappear = 0;
            }
        }
    }

    public void RemoveText()
    {
        _readyToDisappear = true;
    }
}