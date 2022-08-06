using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    public void LoadLevel(int level)
    {
        if (level == 1) Application.LoadLevel("Level 01");
        if (level == 2) Application.LoadLevel("Level 02");
        if (level == 3) Application.LoadLevel("Level 03");
    }
}
