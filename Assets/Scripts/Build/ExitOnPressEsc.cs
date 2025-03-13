using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitOnPressEsc : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
