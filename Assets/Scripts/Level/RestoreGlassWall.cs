using UnityEngine;
using System.Collections;

public class RestoreGlassWall : MonoBehaviour
{
    private Breakable _glassWall;
    private bool _respawn;

	private void Start ()
    {
        StartCoroutine("ActiveGlassWall");
	}
	
    public void SetWall(Breakable previousGlassWall)
    {
        _glassWall = previousGlassWall;
        _respawn = previousGlassWall.respawn;
    }

    private IEnumerator ActiveGlassWall()
    {
        yield return new WaitForSeconds(10f);
        if (_respawn) _glassWall.gameObject.SetActive(true);
        StopCoroutine("ActiveGlassWall");
    }
}