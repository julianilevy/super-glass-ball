using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveFuelBubbles : MonoBehaviour
{
    public Image mask;

    private Vector3 _originalPos;
    private Vector3 _finalPos;
    private float _timer;

	private void Start ()
    {
        _originalPos = new Vector3(178.1f, mask.transform.position.y, mask.transform.position.z);
        _finalPos = new Vector3(-123, mask.transform.position.y, mask.transform.position.z);
	}
	
	private void Update ()
    {
        MoveBubbles();
	}

    private void MoveBubbles()
    {
        _timer += Time.deltaTime / 2;
        mask.transform.localPosition = Vector3.Lerp(_originalPos, _finalPos, _timer);

        if (_timer >= 1)
        {
            _timer = 0;
            mask.transform.localPosition = _originalPos;
        }
    }
}
