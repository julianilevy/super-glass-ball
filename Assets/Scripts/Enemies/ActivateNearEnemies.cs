using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivateNearEnemies : MonoBehaviour
{
    public List<Enemy> nearEnemies = new List<Enemy>();

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == K.LAYER_PLAYER)
        {
            foreach (var enemy in nearEnemies) enemy.viewDistance = 80;
        }
    }
}
