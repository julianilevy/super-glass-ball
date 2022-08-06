using UnityEngine;
using System.Collections;

public class Breakable : MonoBehaviour
{
    public GameObject brokenGlass;
    public bool jumpable;
    public bool respawn;

    public void BreakGlass()
    {
        var brokenGO = (GameObject)Instantiate(brokenGlass, transform.position, transform.rotation) as GameObject;
        brokenGO.transform.localScale = transform.localScale;
        brokenGO.GetComponent<RestoreGlassWall>().SetWall(this);
        Destroy(brokenGO, 10.05f);
        this.gameObject.SetActive(false);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (jumpable)
        {
            if (collision != null)
            {
                if (collision.gameObject.layer == K.LAYER_PLAYER)
                {
                    var rb = collision.gameObject.GetComponent<Player>();
                    rb.Grounded = true;
                    rb.LastHitBreakable = true;

                    if (rb.playerStats.powered) BreakGlass();
                }
            }
        }
    }
}