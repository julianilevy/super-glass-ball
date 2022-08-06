using UnityEngine;
using System.Collections;

public class GermanGlassWall : MonoBehaviour
{
    private Vector3 _firstMoveInitialPos;
    private Vector3 _firstMoveFinalPos;
    private Vector3 _secondMoveInitialPos;
    private Vector3 _secondMoveFinalPos;
    private float _firstMoveTimer;
    private float _secondMoveTimer;
    private bool _firstMove;
    private bool _secondMove;

    private void Start()
    {
        _firstMove = true;
        _firstMoveInitialPos = transform.position;
        _firstMoveFinalPos = transform.position + new Vector3(0, 0.8f, 0);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_firstMove)
        {
            _firstMoveTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(_firstMoveInitialPos, _firstMoveFinalPos, _firstMoveTimer);

            if (_firstMoveTimer >= 1)
            {
                _firstMoveTimer = 0;
                _firstMove = false;
                _secondMoveInitialPos = transform.position;
                _secondMoveFinalPos = transform.position + new Vector3(0, 17.2f, 0);
                StartCoroutine(WaitToLerpAndDestroy());
            }
        }
        if (_secondMove)
        {
            _secondMoveTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(_secondMoveInitialPos, _secondMoveFinalPos, _secondMoveTimer);

            if (_secondMoveTimer >= 1)
            {
                _secondMoveTimer = 0;
                _secondMove = false;
            }
        }
    }

    private IEnumerator WaitToLerpAndDestroy()
    {
        yield return new WaitForSeconds(1.2f);
        _secondMove = true;
        yield return new WaitForSeconds(11f);
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                if (child.gameObject.layer == K.LAYER_GLASS)
                {
                    Breakable breakableChild;
                    breakableChild = child.GetComponent<Breakable>();

                    if (breakableChild is Breakable)
                    {
                        breakableChild.BreakGlass();
                    }
                }
            }
        }
        StopCoroutine(WaitToLerpAndDestroy());
    }
}
