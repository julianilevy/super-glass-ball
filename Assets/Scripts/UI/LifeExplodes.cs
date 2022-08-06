using UnityEngine;
using System.Collections;

public class LifeExplodes : MonoBehaviour
{
    private void Start ()
    {
        Destroy(gameObject.GetComponent<Collider>());
    }

	private void FixedUpdate ()
    {
        ReduceScale();
	}

    private void ReduceScale()
    {
        if (transform.localScale.magnitude >= 0.1f) transform.localScale -= Vector3.one * Time.deltaTime / 3;
        else Destroy(this.gameObject);
    }
}
