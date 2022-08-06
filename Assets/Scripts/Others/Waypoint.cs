using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour
{
    public Waypoint[] neighbourNodes;

    public void OnDrawGizmos()
    {
        /*Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1f);
        Gizmos.color = Color.green;

        for (int i = 0; i < neighbourNodes.Length; i++)
        {
            Gizmos.DrawLine(transform.position, neighbourNodes[i].transform.position);
        }*/
    }
}