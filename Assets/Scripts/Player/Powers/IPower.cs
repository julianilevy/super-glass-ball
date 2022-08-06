using UnityEngine;
using System.Collections;

public interface IPower
{
    void TriggerPower();
    void DoChangeToMetal();
    void DoChangeToGlass();
    void ChangeMeshToMetal();
    void ChangeMeshToGlass();
    void ResetAll();
}