using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pleinecran : MonoBehaviour
{
    public void ToggleFullScreen()
    {
        // V�rifier si le jeu est d�j� en mode plein �cran
        if (Screen.fullScreen)
        {
            // Si oui, quitter le mode plein �cran
            Screen.fullScreen = false;
        }
        else
        {
            // Sinon, activer le mode plein �cran
            Screen.fullScreen = true;
        }
    }
}
