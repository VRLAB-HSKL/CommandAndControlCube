using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitApplication : MonoBehaviour
{
        // Initialisierung der Instanz
        void Start() { }
        // Update wird in jedem Frame ausgeführt!
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
                Application.Quit();
        }

}
