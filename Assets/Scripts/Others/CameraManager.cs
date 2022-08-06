using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public Camera frontCamera;
    public Camera backCamera;
	
	public void Update ()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            frontCamera.gameObject.SetActive(false);
            backCamera.gameObject.SetActive(true);
        }
        else
        {
            frontCamera.gameObject.SetActive(true);
            backCamera.gameObject.SetActive(false);
        }
	}
}
