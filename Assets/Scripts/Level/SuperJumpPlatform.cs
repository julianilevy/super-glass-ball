using UnityEngine;
using System.Collections;

public class SuperJumpPlatform : MonoBehaviour
{
    public float extraJump = 200;

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.layer == K.LAYER_PLAYER)
        {
            collider.GetComponent<Player>().SuperJumpEnabled = true;
            collider.GetComponent<Player>().ExtraJump = extraJump;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == K.LAYER_PLAYER) collider.GetComponent<Player>().SuperJumpEnabled = false;
    }
}
