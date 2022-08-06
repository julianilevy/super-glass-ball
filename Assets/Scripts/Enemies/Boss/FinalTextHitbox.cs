using UnityEngine;
using System.Collections;

public class FinalTextHitbox : MonoBehaviour
{
    public GameDataManager gameDataManager;
    public FinalText finalText;
    public bool activated;

    private void OnTriggerEnter(Collider collider)
    {
        if (!activated)
        {
            activated = true;
            finalText.playerStats.locked = true;
            StartCoroutine(finalText.ActivateTrueFinalText01());
        }
    }
}
